using Castle.MicroKernel;

namespace Cqrs.Simple.Castle.Factories
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IKernel kernel;

        public QueryHandlerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IHandleQuery<TArguments, TResult> Resolve<TArguments, TResult>() where TArguments : IQuery
        {
            return (IHandleQuery<TArguments, TResult>)kernel.Resolve(
                typeof(IHandleQuery<,>).MakeGenericType(typeof(TArguments), typeof(TResult))
            );
        }

        public void Release<TArguments, TResult>(IHandleQuery<TArguments, TResult> handler) where TArguments : IQuery
        {
            kernel.ReleaseComponent(handler);
        }
    }
}