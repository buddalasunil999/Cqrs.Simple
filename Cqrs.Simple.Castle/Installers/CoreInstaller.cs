using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Cqrs.Simple.Castle.Installers
{
    public class CoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IExecute>()
                    .ImplementedBy<Execute>()
                    .LifestyleSingleton()
            );
        }
    }
}