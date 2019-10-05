// -----------------------------------------------------------------------
//  <copyright file="OperationCommandHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using OperationResults;

    public abstract class OperationCommandHandler<TRequest, TResponse> : CommandHandler<TRequest, OperationResult<TResponse>>
            where TRequest : ICommand<OperationResult<TResponse>> { }
}