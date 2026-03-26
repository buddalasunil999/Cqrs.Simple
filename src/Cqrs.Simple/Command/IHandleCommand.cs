using System.Threading.Tasks;

namespace Cqrs.Simple
{
    public interface IHandleCommand<in TArguments, TResult>
        where TArguments : ICommand
    {
        Task<TResult> Handle(TArguments message);
    }

    public interface IHandleCommand<in TArguments>
        where TArguments : ICommand
    {
        Task Handle(TArguments message);
    }
}