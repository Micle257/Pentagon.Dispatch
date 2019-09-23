// -----------------------------------------------------------------------
//  <copyright file="DispatcherServiceCollectionExtensions.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Dispatch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using JetBrains.Annotations;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class DispatcherServiceCollectionExtensions
    {
        public static IServiceCollection AddDispatcher(this IServiceCollection services, bool autoAddCommandHandlers = false, ServiceLifetime scope = ServiceLifetime.Scoped)
        {
            services.Add(ServiceDescriptor.Describe(typeof(IDispatcher), c => new Dispatcher(c), scope));

            if (!autoAddCommandHandlers)
                return services;

            return services.AddDispatchCommandHandlers(ServiceLifetime.Transient);
        }

        public static IServiceCollection AddDispatchPipeline<TPipeline, TRequest, TResponse>(this IServiceCollection services, ServiceLifetime scope = ServiceLifetime.Scoped)
            where TPipeline : IPipelineBehavior<TRequest, TResponse>, new()
        {
            services.Add(ServiceDescriptor.Describe(typeof(IPipelineBehavior<TRequest, TResponse>), typeof(TPipeline), scope));

            return services.AddDispatchCommandHandlers(ServiceLifetime.Transient);
        }

        public static IServiceCollection AddDispatchPipelines(this IServiceCollection services, ServiceLifetime scope = ServiceLifetime.Scoped)
        {
            var commands = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .SelectMany(a => GetLoadableTypes(a))
                                    .Where(a => a.IsClass && !a.IsAbstract)
                                    .Distinct();

            foreach (var command in commands)
            {
                var interf = command.GetInterfaces()
                                    .Where(b => b.GenericTypeArguments.Length == 2)
                                    .FirstOrDefault(a => a.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>));

                if (interf == null)
                    continue;

                services.Add(ServiceDescriptor.Describe(interf, command, scope));
            }

            return services;
        }

        public static IServiceCollection AddDispatchCommandHandlers(this IServiceCollection services, ServiceLifetime scope = ServiceLifetime.Scoped)
        {
            var commands = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .SelectMany(a => GetLoadableTypes(a))
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

        // move to Common
        [NotNull]
        public static IEnumerable<Type> GetLoadableTypes([NotNull] Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}