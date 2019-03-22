using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cqrs.Simple;
using Cqrs.Simple.MicrosoftDI.Factories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the handlers and sessions from root assembly and referenced assemblies starting with same name into service collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="rootAssembly"></param>
        public static void AddCqrs(this IServiceCollection services, Assembly rootAssembly)
        {
            services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddSingleton<IQueryHandlerFactory, QueryHandlerFactory>();
            services.AddSingleton<IAsyncQueryHandlerFactory, AsyncQueryHandlerFactory>();
            services.AddSingleton<IExecute, Execute>();

            var withServiceBaseTypes = new[]
            {
                typeof(IHandleQuery<,>),
                typeof(IHandleQueryAsync<,>),
                typeof(IHandleCommand<>),
                typeof(IHandleCommand<,>)
            };

            var withServiceSelfTypes = new[] { typeof(ISession) };

            var parts = rootAssembly.GetName().Name.Split('.');
            if (parts.Length > 0)
            {
                var assemblies = rootAssembly
                    .GetReferencedAssemblies()
                    .Where(x => x.Name.StartsWith(parts[0]))
                    .Select(Assembly.Load)
                    .Append(rootAssembly)
                    .ToList();

                BasedOn(services, assemblies, withServiceBaseTypes, WithServiceBase);
                BasedOn(services, assemblies, withServiceSelfTypes, WithServiceSelf);
            }
        }

        private static void BasedOn(IServiceCollection services, List<Assembly> assemblies, Type[] types, Action<IServiceCollection, Type, Type> method)
        {
            foreach (Type type in types)
            {
                var allTypes = assemblies
                    .SelectMany(x => x.GetTypes())
                    .Where(x => !x.IsAbstract && !x.IsInterface);

                foreach (Type implementation in allTypes)
                {
                    method(services, implementation, type);
                }
            }
        }

        private static void WithServiceBase(IServiceCollection services, Type implementation, Type type)
        {
            if (implementation.GetInterfaces()
                .Any(x => x.IsGenericType &&
                          x.GetGenericTypeDefinition() == type))
            {
                services.AddTransient(type, implementation);
            }
        }

        private static void WithServiceSelf(IServiceCollection services, Type implementation, Type type)
        {
            if (implementation.GetInterfaces()
                .Any(x => x == type))
            {
                services.AddTransient(implementation);
            }
        }
    }
}