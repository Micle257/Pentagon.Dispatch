// -----------------------------------------------------------------------
//  <copyright file="IQueryDispatcher.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Queries
{
    using System.Threading.Tasks;

    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;
    }
}