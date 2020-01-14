// -----------------------------------------------------------------------
//  <copyright file="EventDispatcher.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Events
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    sealed class EventDispatcher : IEventDispatcher
    {
        readonly IServiceScopeFactory _serviceFactory;

        public EventDispatcher(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken)
                where T : class, IEvent
        {
            using var scope    = _serviceFactory.CreateScope();
            var       handlers = scope.ServiceProvider.GetServices<IEventHandler<T>>();
            foreach (var handler in handlers)
                await handler.HandleAsync(@event: @event, cancellationToken);
        }
    }
}