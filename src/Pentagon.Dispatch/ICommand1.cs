// -----------------------------------------------------------------------
//  <copyright file="ICommand1.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    public interface ICommand<out TResponse> : ICommand { }
}