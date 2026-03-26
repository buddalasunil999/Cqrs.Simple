using System.Threading.Tasks;

namespace Cqrs.Simple
{
    public interface IExecute
    {
        Task Command(ICommand arguments);
        Task<TResult> Command<TArguments, TResult>(TArguments arguments)
            where TArguments : ICommand;

        TResult Query<TArguments, TResult>(TArguments arguments)
            where TArguments : IQuery;
        Task<TResult> QueryAsync<TArguments, TResult>(TArguments arguments)
            where TArguments : IQuery;

        Task<TResult> Command<TResult>(ICommand<TResult> arguments);
        TResult Query<TResult>(IQuery<TResult> arguments);
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> arguments);
    }
}