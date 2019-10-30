// -----------------------------------------------------------------------
//  <copyright file="IDispatcherFacade.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using Commands;
    using Events;
    using Queries;

    /// <summary> Provides a facade for all dispatcher types. </summary>
    public interface IDispatcherFacade : ICommandDispatcher, IQueryDispatcher, IEventDispatcher { }
}