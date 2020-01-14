// -----------------------------------------------------------------------
//  <copyright file="CommandHandler1.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Dispatch.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Pentagon.Dispatch.Queries;

    public class CommandHandler1 : IQueryHandler<Command1, Response1>
    {
        /// <inheritdoc />
        public Task<Response1> HandleAsync(Command1 query, CancellationToken cancellationToken) => Task.FromResult(new Response1());
    }
}