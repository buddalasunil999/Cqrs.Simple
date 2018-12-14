using Castle.MicroKernel;

namespace Cqrs.Simple
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IKernel kernel;

        public CommandHandlerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IHandleCommand<TArguments> Resolve<TArguments>() where TArguments : ICommand
        {
            return kernel.Resolve<IHandleCommand<TArguments>>();
        }

        public IHandleCommand<TArguments, TResult> Resolve<TArguments, TResult>() where TArguments : ICommand
        {
            return kernel.Resolve<IHandleCommand<TArguments, TResult>>();
        }

        public void Release<TArguments>(IHandleCommand<TArguments> handler) where TArguments : ICommand
        {
            kernel.ReleaseComponent(handler);
        }

        public void Release<TArguments, TResult>(IHandleCommand<TArguments, TResult> handler) where TArguments : ICommand
        {
            kernel.ReleaseComponent(handler);
        }
    }
}