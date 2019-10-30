// -----------------------------------------------------------------------
//  <copyright file="ICommandHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using OperationResults;

    public interface ICommandHandler<in TCommand>
            where TCommand : class, ICommand
    {
        Task<OperationResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}