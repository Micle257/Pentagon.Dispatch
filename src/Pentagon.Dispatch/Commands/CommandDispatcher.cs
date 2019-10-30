// -----------------------------------------------------------------------
//  <copyright file="CommandDispatcher.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using OperationResults;

    sealed class CommandDispatcher : ICommandDispatcher
    {
        readonly IServiceScopeFactory _serviceFactory;

        public CommandDispatcher(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task<OperationResult> SendAsync<T>(T command, CancellationToken cancellationToken)
                where T : class, ICommand
        {
            using var scope = _serviceFactory.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>();

            return await handler.HandleAsync(command: command, cancellationToken: cancellationToken);
        }
    }
}