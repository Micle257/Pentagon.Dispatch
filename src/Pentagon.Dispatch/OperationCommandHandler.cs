// -----------------------------------------------------------------------
//  <copyright file="OperationCommandHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    public abstract class OperationCommandHandler<TRequest, TResponse> : CommandHandler<TRequest, OperationResult<TResponse>>
            where TRequest : ICommand<OperationResult<TResponse>> { }
}