// -----------------------------------------------------------------------
//  <copyright file="DispatcherTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Dispatch.Tests
{
    using System.Threading;
    using Microsoft.Extensions.DependencyInjection;
    using Pentagon.Dispatch.Queries;
    using Threading;
    using Xunit;

    public class DispatcherTests
    {
        [Fact]
        public void AddDispatcher_ArgumentValueTrue_AddsHandlerToProvider()
        {
            var ser = new ServiceCollection()
                   .AddTransient<IQueryHandler<Command1, Response1>, CommandHandler1>();

            var di = ser.BuildServiceProvider();

            var service = new QueryDispatcher(di.GetRequiredService<IServiceScopeFactory>());

            var response1 = service.QueryAsync(new Command1(), CancellationToken.None).AwaitSynchronously();
        }
    }
}