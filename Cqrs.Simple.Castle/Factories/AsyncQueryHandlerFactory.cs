using Castle.MicroKernel;

namespace Cqrs.Simple.Castle.Factories
{
    public class AsyncQueryHandlerFactory : IAsyncQueryHandlerFactory
    {
        private readonly IKernel kernel;

        public AsyncQueryHandlerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IHandleQueryAsync<TArguments, TResult> Resolve<TArguments, TResult>() where TArguments : IQuery
        {
            return (IHandleQueryAsync<TArguments, TResult>)kernel.Resolve(
                typeof(IHandleQueryAsync<,>).MakeGenericType(typeof(TArguments), typeof(TResult))
            );
        }

        public void Release<TArguments, TResult>(IHandleQueryAsync<TArguments, TResult> handler) where TArguments : IQuery
        {
            kernel.ReleaseComponent(handler);
        }
    }
}