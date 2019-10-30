// -----------------------------------------------------------------------
//  <copyright file="ICommandDispatcher.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using OperationResults;

    public interface ICommandDispatcher
    {
        Task<OperationResult> SendAsync<T>(T command, CancellationToken cancellationToken = default)
                where T : class, ICommand;
    }
}