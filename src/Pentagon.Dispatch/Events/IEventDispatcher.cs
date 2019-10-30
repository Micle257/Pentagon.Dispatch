// -----------------------------------------------------------------------
//  <copyright file="IEventDispatcher.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Events
{
    using System.Threading.Tasks;

    public interface IEventDispatcher
    {
        Task PublishAsync<T>(T @event)
                where T : class, IEvent;
    }
}