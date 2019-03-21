using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Cqrs.Simple.Castle.Factories;

namespace Cqrs.Simple.Castle.Installers
{
    public class CoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICommandHandlerFactory>()
                    .ImplementedBy<CommandHandlerFactory>()
                    .LifestyleSingleton(),
                Component.For<IQueryHandlerFactory>()
                    .ImplementedBy<QueryHandlerFactory>()
                    .LifestyleSingleton(),
                Component.For<IAsyncQueryHandlerFactory>()
                    .ImplementedBy<AsyncQueryHandlerFactory>()
                    .LifestyleSingleton(),
                Component.For<IExecute>()
                    .ImplementedBy<Execute>()
                    .LifestyleSingleton()
            );
        }
    }
}