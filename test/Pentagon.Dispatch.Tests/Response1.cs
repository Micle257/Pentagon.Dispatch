// -----------------------------------------------------------------------
//  <copyright file="Response1.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Dispatch.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Pentagon.Dispatch;
    using Xunit;

    public class Response1 { }

    public class Pipeline1 : IPipelineBehavior<Command1, Response1>
    {
        /// <inheritdoc />
        public Task<Response1> Handle(Command1 request, CancellationToken cancellationToken, CommandHandlerDelegate<Response1> next)
        {
            return next();
        }
    }

    public class Pipeline2 : IPipelineBehavior<Command1, Response1>
    {
        /// <inheritdoc />
        public Task<Response1> Handle(Command1 request, CancellationToken cancellationToken, CommandHandlerDelegate<Response1> next)
        {
            return next();
        }
    }

    public class Pipeline3 : IPipelineBehavior<Command1, Response1>
    {
        /// <inheritdoc />
        public Task<Response1> Handle(Command1 request, CancellationToken cancellationToken, CommandHandlerDelegate<Response1> next)
        {
            return next();
        }
    }

    public class CommandHandlerWrapperTests
    {
        [Fact]
        public void d()
        {
            var services = new ServiceCollection().AddDispatcher()
                                                  .AddDispatchCommandHandlers()
                                                  .AddDispatchPipelines();

            var di = services.BuildServiceProvider();

            var commandHandlerWrapper = new CommandHandlerWrapper<Command1, Response1>();

            commandHandlerWrapper.Handle(new Command1(), CancellationToken.None, di);
        }
    }
}