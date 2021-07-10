using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Cqrs.Simple.Castle.Installers
{
    public class CqrsInstaller : IWindsorInstaller
    {
        private readonly List<string> assemblyMatches;

        public CqrsInstaller(List<string> assemblyMatches = null)
        {
            this.assemblyMatches = assemblyMatches;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (assemblyMatches != null)
            {
                var matchingAssemblies = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(x => assemblyMatches.Contains(x.FullName.Split('.')[0]));

                foreach (var assembly in matchingAssemblies)
                {
                    container.Register(GetRegistrations(Classes.FromAssembly(assembly)).ToArray());
                }
            }

            var entryAssembly = Assembly.GetEntryAssembly();
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => x.FullName.StartsWith(entryAssembly.FullName.Split('.')[0]));

            foreach (var assembly in assemblies)
            {
                container.Register(GetRegistrations(Classes.FromAssembly(assembly)).ToArray());
            }
        }

        private static IEnumerable<IRegistration> GetRegistrations(FromAssemblyDescriptor fromAssembly)
        {
            yield return fromAssembly
                .BasedOn(typeof(IHandleQuery<,>))
                .WithServiceBase()
                .LifestyleTransient();

            yield return fromAssembly
                .BasedOn(typeof(IHandleQueryAsync<,>))
                .WithServiceBase()
                .LifestyleTransient();

            yield return fromAssembly
                .BasedOn(typeof(IHandleCommand<>))
                .WithServiceBase()
                .LifestyleTransient();

            yield return fromAssembly
                .BasedOn(typeof(IHandleCommand<,>))
                .WithServiceBase()
                .LifestyleTransient();

            yield return fromAssembly
                .BasedOn<ISession>()
                .WithServiceSelf()
                .WithServiceAllInterfaces()
                .LifestyleScoped();
        }
    }
}