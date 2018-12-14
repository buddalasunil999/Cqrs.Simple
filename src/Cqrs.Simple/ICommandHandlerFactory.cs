namespace Cqrs.Simple
{
    public interface ICommandHandlerFactory
    {
        IHandleCommand<TArguments> Resolve<TArguments>()
            where TArguments : ICommand;
        IHandleCommand<TArguments, TResult> Resolve<TArguments, TResult>()
            where TArguments : ICommand;
        void Release<TArguments>(IHandleCommand<TArguments> handler)
            where TArguments : ICommand;
        void Release<TArguments, TResult>(IHandleCommand<TArguments, TResult> handler)
            where TArguments : ICommand;
    }
}