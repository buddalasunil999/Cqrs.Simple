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
        private readonly Assembly[] assemblies;

        public CqrsInstaller(params Assembly[] assemblies)
        {
            this.assemblies = assemblies;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
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
                .LifestyleScoped();
        }
    }
}