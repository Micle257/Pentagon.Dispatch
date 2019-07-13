// -----------------------------------------------------------------------
//  <copyright file="CommandHandlerDelegate.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System.Threading.Tasks;

    public delegate Task<TResponse> CommandHandlerDelegate<TResponse>();
}