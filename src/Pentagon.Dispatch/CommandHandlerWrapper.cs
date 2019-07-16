// -----------------------------------------------------------------------
//  <copyright file="CommandHandlerWrapper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Microsoft.Extensions.DependencyInjection;

    abstract class CommandHandlerWrapper
    {
        protected static THandler GetHandler<THandler>(IServiceProvider services)
                where THandler : class
        {
            THandler handler;

            try
            {
                handler = (THandler)services.GetService(typeof(THandler)); ;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error constructing handler for request of type {typeof(THandler)}. Register your handlers with the container.", e);
            }

            if (handler == null)
            {
                throw new InvalidOperationException($"Handler was not found for request of type {typeof(THandler)}. Register your handlers with the container.");
            }

            return handler;
        }
    }

    abstract class CommandHandlerWrapper<TResponse> : CommandHandlerWrapper
    {
        public abstract Task<TResponse> Handle(ICommand<TResponse> request,
                                               CancellationToken cancellationToken,
                                               IServiceProvider serviceFactory);
    }

    class CommandHandlerWrapper<TRequest, TResponse> : CommandHandlerWrapper<TResponse>
            where TRequest : ICommand<TResponse>
    {
        public override Task<TResponse> Handle(ICommand<TResponse> command,
                                      CancellationToken cancellationToken,
                                      IServiceProvider serviceFactory)
        {
            var handler = GetHandler<ICommandHandler<TRequest, TResponse>>(serviceFactory);

            Task<TResponse> Handler() => handler.ExecuteAsync((TRequest) command, cancellationToken);

            return serviceFactory
                   .GetServices<IPipelineBehavior<TRequest, TResponse>>()
                   .Reverse()
                   .Aggregate(Handler, Aggragate(command, cancellationToken))();

            Func<CommandHandlerDelegate<TResponse>, IPipelineBehavior<TRequest, TResponse>, CommandHandlerDelegate<TResponse>> Aggragate(ICommand<TResponse> command1, CancellationToken cancellationToken1)
            {
                return (next, pipeline) => () => pipeline.Handle((TRequest) command1, cancellationToken1, next);
            }
        }
    }
}