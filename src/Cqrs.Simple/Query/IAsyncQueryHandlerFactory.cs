namespace Cqrs.Simple
{
    public interface IAsyncQueryHandlerFactory
    {
        IHandleQueryAsync<TArguments, TResult> Resolve<TArguments, TResult>()
            where TArguments : IQuery;

        void Release<TArguments, TResult>(IHandleQueryAsync<TArguments, TResult> handler)
            where TArguments : IQuery;
    }
}