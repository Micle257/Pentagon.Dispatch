// -----------------------------------------------------------------------
//  <copyright file="DispatcherFacade.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System.Threading;
    using System.Threading.Tasks;
    using Commands;
    using Events;
    using JetBrains.Annotations;
    using OperationResults;
    using Queries;

    /// <summary> Default implementation. </summary>
    sealed class DispatcherFacade : IDispatcherFacade
    {
        [NotNull]
        readonly ICommandDispatcher _commandDispatcher;

        [NotNull]
        readonly IQueryDispatcher _queryDispatcher;

        [NotNull]
        readonly IEventDispatcher _eventDispatcher;

        public DispatcherFacade([NotNull] ICommandDispatcher commandDispatcher,
                                [NotNull] IQueryDispatcher queryDispatcher,
                                [NotNull] IEventDispatcher eventDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher   = queryDispatcher;
            _eventDispatcher   = eventDispatcher;
        }

        /// <inheritdoc />
        public Task<OperationResult> SendAsync<T>(T command, CancellationToken cancellationToken = default)
                where T : class, ICommand =>
                _commandDispatcher.SendAsync(command: command, cancellationToken: cancellationToken);

        /// <inheritdoc />
        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query) => _queryDispatcher.QueryAsync(query: query);

        /// <inheritdoc />
        public Task<TResult> QueryAsync<TQuery, TResult>(TQuery query)
                where TQuery : class, IQuery<TResult> =>
                _queryDispatcher.QueryAsync(query: query);

        /// <inheritdoc />
        public Task PublishAsync<T>(T @event)
                where T : class, IEvent =>
                _eventDispatcher.PublishAsync(@event: @event);
    }
}