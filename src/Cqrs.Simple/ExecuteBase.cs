using System;
using System.Threading.Tasks;

namespace Cqrs.Simple
{
    public abstract class ExecuteBase : IExecute
    {
        public TResult Query<TArguments, TResult>(TArguments arguments) where TArguments : IQuery
        {
            var handler = (IHandleQuery<TArguments, TResult>)Resolve(
                typeof(IHandleQuery<,>).MakeGenericType(typeof(TArguments), typeof(TResult))
            );

            if (handler == null)
            {
                throw new Exception($"Unable to resolve for {typeof(TArguments).FullName}, {typeof(TResult).FullName}");
            }

            try
            {
                return handler.Handle(arguments);
            }
            finally
            {
                //Release(handler);
            }
        }

        public async Task<TResult> QueryAsync<TArguments, TResult>(TArguments arguments) where TArguments : IQuery
        {
            var handler = (IHandleQueryAsync<TArguments, TResult>)Resolve(
                typeof(IHandleQueryAsync<,>).MakeGenericType(typeof(TArguments), typeof(TResult))
            );

            if (handler == null)
            {
                throw new Exception($"Unable to resolve for {typeof(TArguments).FullName}, {typeof(TResult).FullName}");
            }

            try
            {
                return await handler.Handle(arguments);
            }
            finally
            {
                //Release(handler);
            }
        }

        public async Task Command(ICommand argument)
        {
            var baseType = typeof(IHandleCommand<>);

            var handlerType = baseType.MakeGenericType(argument.GetType());
            var handler = Resolve(handlerType);

            if (handler == null)
            {
                throw new Exception($"Unable to resolve for {argument.GetType().FullName}");
            }

            try
            {
                var method = handler.GetType().GetMethod("Handle");

                await (Task)method.Invoke(handler, new object[] { argument });
            }
            finally
            {
                //Release(handler);
            }
        }

        public async Task<TResult> Command<TArguments, TResult>(TArguments arguments) where TArguments : ICommand
        {
            var handler = Resolve<IHandleCommand<TArguments, TResult>>();
            if (handler == null)
            {
                throw new Exception($"Unable to resolve for {typeof(TArguments).FullName}, {typeof(TResult).FullName}");
            }

            try
            {
                return await handler.Handle(arguments);
            }
            finally
            {
                //Release(handler);
            }
        }

        public TResult Query<TResult>(IQuery<TResult> arguments)
        {
            var baseType = typeof(IHandleQuery<,>);

            return HandleSync<IQuery<TResult>, TResult>(baseType, arguments);
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> arguments)
        {
            var baseType = typeof(IHandleQueryAsync<,>);

            return await Handle<IQuery<TResult>, TResult>(baseType, arguments);
        }

        public async Task<TResult> Command<TResult>(ICommand<TResult> arguments)
        {
            var baseType = typeof(IHandleCommand<,>);

            return await Handle<ICommand<TResult>, TResult>(baseType, arguments);
        }

        protected abstract object Resolve(Type type);
        protected abstract T Resolve<T>();

        private TResult HandleSync<TArgument, TResult>(Type baseType, TArgument argument)
        {
            var handlerType = baseType.MakeGenericType(argument.GetType(), typeof(TResult));
            var handler = Resolve(handlerType);

            if (handler == null)
            {
                throw new Exception($"Unable to resolve for {argument.GetType().FullName}, {typeof(TResult).FullName}");
            }

            try
            {
                var method = handler.GetType().GetMethod("Handle");

                return (TResult)method.Invoke(handler, new object[] { argument });
            }
            finally
            {
                //Release(handler);
            }
        }

        private async Task<TResult> Handle<TArgument, TResult>(Type baseType, TArgument argument)
        {
            var handlerType = baseType.MakeGenericType(argument.GetType(), typeof(TResult));
            var handler = Resolve(handlerType);

            if (handler == null)
            {
                throw new Exception($"Unable to resolve for {argument.GetType().FullName}, {typeof(TResult).FullName}");
            }

            try
            {
                var method = handler.GetType().GetMethod("Handle");

                return await (Task<TResult>)method.Invoke(handler, new object[] { argument });
            }
            finally
            {
                //Release(handler);
            }
        }
    }
}