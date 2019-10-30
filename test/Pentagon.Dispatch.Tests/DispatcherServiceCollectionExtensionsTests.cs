// -----------------------------------------------------------------------
//  <copyright file="DispatcherServiceCollectionExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Dispatch.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Pentagon.Dispatch;
    using Pentagon.Dispatch.Queries;
    using Xunit;

    public class DispatcherServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddDispatcher_AddsHandlerToProvider()
        {
            var ser = new ServiceCollection();

            ser.AddInMemoryQueryDispatcher()
               .AddQueryHandlers();

            var di = ser.BuildServiceProvider();

            var handler = di.GetService<IQueryHandler<Command1, Response1>>();

            Assert.NotNull(@object: handler);
        }
    }
}