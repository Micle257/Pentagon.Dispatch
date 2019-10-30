// -----------------------------------------------------------------------
//  <copyright file="IEventHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Events
{
    using System.Threading.Tasks;

    public interface IEventHandler<in TEvent>
            where TEvent : class, IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}