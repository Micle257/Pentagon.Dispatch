// -----------------------------------------------------------------------
//  <copyright file="DispatcherServiceCollectionExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;

    public static class DispatcherServiceCollectionExtensions
    {
        public static IServiceCollection AddDispatcher(this IServiceCollection services, bool autoAddCommandHandlers = false, ServiceLifetime scope = ServiceLifetime.Scoped)
        {
            services.AddSingleton<IDispatcher>(c => new Dispatcher(c));

            if (!autoAddCommandHandlers)
                return services;

            var commands = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .SelectMany(a => a.GetTypes())
                                    .Where(a => a.IsClass && !a.IsAbstract)
                                    .Distinct();

            foreach (var command in commands)
            {
                var interf = command.GetInterfaces()
                                    .Where(b => b.GenericTypeArguments.Length == 2)
                                    .FirstOrDefault(a => a.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));

                if (interf == null)
                    continue;

                services.Add(ServiceDescriptor.Describe(interf, command, scope));
            }

            return services;
        }
    }
}