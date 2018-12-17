using System.Threading.Tasks;

namespace Cqrs.Simple
{
    public interface IExecute
    {
        Task Command<TArguments>(TArguments arguments)
            where TArguments : ICommand;
        Task<TResult> Command<TArguments, TResult>(TArguments arguments)
            where TArguments : ICommand;
        TResult Query<TArguments, TResult>(TArguments arguments)
            where TArguments : IQuery;
        Task<TResult> QueryAsync<TArguments, TResult>(TArguments arguments)
            where TArguments : IQuery;
    }
}