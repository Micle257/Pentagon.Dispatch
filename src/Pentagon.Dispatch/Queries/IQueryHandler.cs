// -----------------------------------------------------------------------
//  <copyright file="IQueryHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    public interface IQueryHandler<in TQuery, TResult>
            where TQuery : class, IQuery<TResult>
    {
        Task<TResult> HandleAsync([NotNull] TQuery query, CancellationToken cancellationToken);
    }
}