// -----------------------------------------------------------------------
//  <copyright file="Dispatcher.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class Dispatcher : IDispatcher
    {
        static readonly ConcurrentDictionary<Type, object> _requestHandlers = new ConcurrentDictionary<Type, object>();

        readonly IServiceProvider _serviceFactory;

        /// <summary> Initializes a new instance of the <see cref="Dispatcher" /> class. </summary>
        /// <param name="serviceFactory"> The single instance factory. </param>
        public Dispatcher(IServiceProvider serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public Task<TResponse> ExecuteCommandAsync<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var requestType = request.GetType();

            var handler = (CommandHandlerWrapper<TResponse>)_requestHandlers.GetOrAdd(requestType,
                                                                                                 t => Activator.CreateInstance(typeof(CommandHandlerWrapper<,>).MakeGenericType(requestType, typeof(TResponse))));

            return handler.Handle(request, cancellationToken, _serviceFactory);
        }
    }
}