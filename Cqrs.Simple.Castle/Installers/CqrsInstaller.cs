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
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
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