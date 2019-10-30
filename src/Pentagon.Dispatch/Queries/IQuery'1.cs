// -----------------------------------------------------------------------
//  <copyright file="IQuery'1.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Queries
{
    /// <summary> Represents a marker for query request with typed response. </summary>
    public interface IQuery<out TResponse> : IQuery { }
}