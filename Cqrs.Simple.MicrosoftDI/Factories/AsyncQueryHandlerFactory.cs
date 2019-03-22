using System;

namespace Cqrs.Simple.MicrosoftDI.Factories
{
    public class AsyncQueryHandlerFactory : IAsyncQueryHandlerFactory
    {
        private readonly IServiceProvider serviceProvider;

        public AsyncQueryHandlerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IHandleQueryAsync<TArguments, TResult> Resolve<TArguments, TResult>() where TArguments : IQuery
        {
            return (IHandleQueryAsync<TArguments, TResult>)serviceProvider.GetService(
                typeof(IHandleQueryAsync<,>).MakeGenericType(typeof(TArguments), typeof(TResult))
            );
        }

        public void Release<TArguments, TResult>(IHandleQueryAsync<TArguments, TResult> handler) where TArguments : IQuery
        {
        }
    }
}