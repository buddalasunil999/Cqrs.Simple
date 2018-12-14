namespace Cqrs.Simple
{
    public interface IQueryHandlerFactory
    {
        IHandleQuery<TArguments, TResult> Resolve<TArguments, TResult>()
            where TArguments : IQuery;

        void Release<TArguments, TResult>(IHandleQuery<TArguments, TResult> handler)
            where TArguments : IQuery;
    }
}