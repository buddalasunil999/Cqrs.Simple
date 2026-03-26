namespace Cqrs.Simple
{
    public interface ICommand
    { }

    public interface ICommand<TResult> : ICommand
    { }
}