using System;

namespace Cqrs.Simple.MicrosoftDI.Factories
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IServiceProvider kernel;

        public QueryHandlerFactory(IServiceProvider kernel)
        {
            this.kernel = kernel;
        }

        public IHandleQuery<TArguments, TResult> Resolve<TArguments, TResult>() where TArguments : IQuery
        {
            return (IHandleQuery<TArguments, TResult>)kernel.GetService(
                typeof(IHandleQuery<,>).MakeGenericType(typeof(TArguments), typeof(TResult))
            );
        }

        public void Release<TArguments, TResult>(IHandleQuery<TArguments, TResult> handler) where TArguments : IQuery
        {
        }
    }
}