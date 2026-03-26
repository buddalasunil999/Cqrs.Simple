using System.Threading.Tasks;

namespace Cqrs.Simple
{
    public interface IHandleQueryAsync<in TArguments, TResult>
        where TArguments : IQuery
    {
        Task<TResult> Handle(TArguments message);
    }
}