// -----------------------------------------------------------------------
//  <copyright file="CommandHandler1.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Dispatch.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Pentagon.Dispatch;

    public class CommandHandler1 : CommandHandler<Command1, Response1>
    {
        /// <inheritdoc />
        public override Task<Response1> ExecuteAsync(Command1 request, CancellationToken cancellationToken) => Task.FromResult(new Response1());
    }
}