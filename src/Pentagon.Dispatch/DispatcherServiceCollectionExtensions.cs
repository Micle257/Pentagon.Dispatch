// -----------------------------------------------------------------------
//  <copyright file="DispatcherServiceCollectionExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System;
    using System.Linq;
    using Commands;
    using Events;
    using Helpers;
    using JetBrains.Annotations;
    using Microsoft.Extensions.DependencyInjection;
    using Queries;

    public static class DispatcherServiceCollectionExtensions
    {
        [NotNull]
        public static IServiceCollection AddDispatcherFacade([NotNull] this IServiceCollection services)
        {
            services.AddSingleton<IDispatcherFacade, DispatcherFacade>();

            return services;
        }

        [NotNull]
        public static IServiceCollection AddInMemoryCommandDispatcher([NotNull] this IServiceCollection services)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

            return services;
        }

        [NotNull]
        public static IServiceCollection AddCommandHandlers([NotNull] this IServiceCollection services)
        {
            var commands = AppDomain.CurrentDomain
                                    .GetLoadedTypes()
                                    .Where(a => a.IsClass && !a.IsAbstract)
                                    .Distinct();

            foreach (var command in commands)
            {
                var interfaces = command.GetInterfaces()
                                        .Where(b => b.GenericTypeArguments.Length == 1)
                                        .FirstOrDefault(a => a.GetGenericTypeDefinition() == typeof(ICommandHandler<>));

                if (interfaces == null)
                    continue;

                services.Add(ServiceDescriptor.Describe(serviceType: interfaces, implementationType: command, lifetime: ServiceLifetime.Singleton));
            }

            return services;
        }

        [NotNull]
        public static IServiceCollection AddInMemoryQueryDispatcher([NotNull] this IServiceCollection services)
        {
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

            return services;
        }

        [NotNull]
        public static IServiceCollection AddQueryHandlers([NotNull] this IServiceCollection services)
        {
            var commands = AppDomain.CurrentDomain
                                    .GetLoadedTypes()
                                    .Where(a => a.IsClass && !a.IsAbstract)
                                    .Distinct();

            foreach (var command in commands)
            {
                var interfaces = command.GetInterfaces()
                                        .Where(b => b.GenericTypeArguments.Length == 2)
                                        .FirstOrDefault(a => a.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));

                if (interfaces == null)
                    continue;

                services.Add(ServiceDescriptor.Describe(serviceType: interfaces, implementationType: command, lifetime: ServiceLifetime.Singleton));
            }

            return services;
        }

        [NotNull]
        public static IServiceCollection AddInMemoryEventDispatcher([NotNull] this IServiceCollection services)
        {
            services.AddSingleton<IEventDispatcher, EventDispatcher>();

            return services;
        }

        [NotNull]
        public static IServiceCollection AddEventHandlers([NotNull] this IServiceCollection services)
        {
            var commands = AppDomain.CurrentDomain
                                    .GetLoadedTypes()
                                    .Where(a => a.IsClass && !a.IsAbstract)
                                    .Distinct();

            foreach (var command in commands)
            {
                var interfaces = command.GetInterfaces()
                                        .Where(b => b.GenericTypeArguments.Length == 1)
                                        .FirstOrDefault(a => a.GetGenericTypeDefinition() == typeof(IEventHandler<>));

                if (interfaces == null)
                    continue;

                services.Add(ServiceDescriptor.Describe(serviceType: interfaces, implementationType: command, lifetime: ServiceLifetime.Singleton));
            }

            return services;
        }
    }
}