// -----------------------------------------------------------------------
//  <copyright file="IQueryHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Queries
{
    using System.Threading.Tasks;

    public interface IQueryHandler<in TQuery, TResult>
            where TQuery : class, IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}