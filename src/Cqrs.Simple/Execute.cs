using System;
using System.Threading.Tasks;

namespace Cqrs.Simple
{
    public class Execute : IExecute
    {
        private readonly IQueryHandlerFactory queryHandlerFactory;
        private readonly IAsyncQueryHandlerFactory asyncQueryHandlerFactory;
        private readonly ICommandHandlerFactory commandHandlerFactory;

        public Execute(ICommandHandlerFactory commandHandlerFactory, 
            IQueryHandlerFactory queryHandlerFactory,
            IAsyncQueryHandlerFactory asyncQueryHandlerFactory)
        {
            this.queryHandlerFactory = queryHandlerFactory ?? throw new ArgumentNullException(nameof(queryHandlerFactory));
            this.asyncQueryHandlerFactory = asyncQueryHandlerFactory ?? throw new ArgumentNullException(nameof(asyncQueryHandlerFactory));
            this.commandHandlerFactory = commandHandlerFactory ?? throw new ArgumentNullException(nameof(commandHandlerFactory));
        }

        public async Task Command<TArguments>(TArguments arguments) where TArguments : ICommand
        {
            var handler = commandHandlerFactory.Resolve<TArguments>();
            try
            {
                await handler.Handle(arguments);
            }
            finally
            {
                commandHandlerFactory.Release(handler);
            }
        }

        public async Task<TResult> Command<TArguments, TResult>(TArguments arguments) where TArguments : ICommand
        {
            var handler = commandHandlerFactory.Resolve<TArguments, TResult>();
            try
            {
                return await handler.Handle(arguments);
            }
            finally
            {
                commandHandlerFactory.Release(handler);
            }
        }

        public TResult Query<TArguments, TResult>(TArguments arguments) where TArguments : IQuery
        {
            var handlers = queryHandlerFactory.Resolve<TArguments, TResult>();
            try
            {
                return handlers.Handle(arguments);
            }
            finally
            {
                queryHandlerFactory.Release(handlers);
            }
        }

        public async Task<TResult> QueryAsync<TArguments, TResult>(TArguments arguments) where TArguments : IQuery
        {
            var handler = asyncQueryHandlerFactory.Resolve<TArguments, TResult>();
            try
            {
                return await handler.Handle(arguments);
            }
            finally
            {
                asyncQueryHandlerFactory.Release(handler);
            }
        }
    }
}