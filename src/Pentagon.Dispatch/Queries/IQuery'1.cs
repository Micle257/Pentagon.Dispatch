// -----------------------------------------------------------------------
//  <copyright file="IQuery'1.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch.Queries
{
    public interface IQuery<out TResponse> : IQuery { }
}