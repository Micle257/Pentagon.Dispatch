// -----------------------------------------------------------------------
//  <copyright file="QueryDispatcher.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    sealed class QueryDispatcher : IQueryDispatcher
    {
        readonly IServiceScopeFactory _serviceFactory;

        public QueryDispatcher(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        {
            using var scope       = _serviceFactory.CreateScope();
            var       handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic   handler     = scope.ServiceProvider.GetRequiredService(serviceType: handlerType);

            return await handler.HandleAsync((dynamic) query, cancellationToken);
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
                where TQuery : class, IQuery<TResult>
        {
            using var scope   = _serviceFactory.CreateScope();
            var       handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await handler.HandleAsync(query: query, cancellationToken );
        }
    }
}