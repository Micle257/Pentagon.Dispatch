// -----------------------------------------------------------------------
//  <copyright file="IDispatcher.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDispatcher
    {
        Task<TResponse> ExecuteCommandAsync<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken = default);
    }
}