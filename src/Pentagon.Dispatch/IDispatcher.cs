// -----------------------------------------------------------------------
//  <copyright file="IDispatcher.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDispatcher
    {
        Task<TResponse> ExecuteCommandAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
                where TRequest : ICommand<TResponse>;
    }
}