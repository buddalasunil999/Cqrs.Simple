using System.Threading.Tasks;

namespace Cqrs.Simple
{
    public abstract class DatabaseAsyncQueryHandler<TArguments, TResult> : IHandleQueryAsync<TArguments, TResult>
        where TArguments : IQuery<TResult>
    {
        protected readonly string Script;
        protected readonly ISession Session;

        protected DatabaseAsyncQueryHandler(ISession session)
        {
            Session = session;
            Script = GetType().GetScript();
        }

        public abstract Task<TResult> Handle(TArguments message);
    }
}