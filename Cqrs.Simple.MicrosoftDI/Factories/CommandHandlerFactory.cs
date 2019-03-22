using System;

namespace Cqrs.Simple.MicrosoftDI.Factories
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider kernel;

        public CommandHandlerFactory(IServiceProvider kernel)
        {
            this.kernel = kernel;
        }

        public IHandleCommand<TArguments> Resolve<TArguments>() where TArguments : ICommand
        {
            return kernel.GetService<IHandleCommand<TArguments>>();
        }

        public IHandleCommand<TArguments, TResult> Resolve<TArguments, TResult>() where TArguments : ICommand
        {
            return kernel.GetService<IHandleCommand<TArguments, TResult>>();
        }

        public void Release<TArguments>(IHandleCommand<TArguments> handler) where TArguments : ICommand
        {
        }

        public void Release<TArguments, TResult>(IHandleCommand<TArguments, TResult> handler) where TArguments : ICommand
        {
        }
    }
}