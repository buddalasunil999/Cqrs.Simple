using System;
using Cqrs.Simple.MicrosoftDI.Extensions;

namespace Cqrs.Simple.MicrosoftDI.Factories
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider serviceProvider;

        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IHandleCommand<TArguments> Resolve<TArguments>() where TArguments : ICommand
        {
            return serviceProvider.GetService<IHandleCommand<TArguments>>();
        }

        public IHandleCommand<TArguments, TResult> Resolve<TArguments, TResult>() where TArguments : ICommand
        {
            return serviceProvider.GetService<IHandleCommand<TArguments, TResult>>();
        }

        public void Release<TArguments>(IHandleCommand<TArguments> handler) where TArguments : ICommand
        {
        }

        public void Release<TArguments, TResult>(IHandleCommand<TArguments, TResult> handler) where TArguments : ICommand
        {
        }
    }
}