namespace Cqrs.Simple
{
    public abstract class DatabaseQueryHandler<TArguments, TResult> : IHandleQuery<TArguments, TResult>
        where TArguments : IQuery
    {
        protected readonly string Script;
        protected readonly ISession Session;

        protected DatabaseQueryHandler(ISession session)
        {
            Session = session;
            Script = GetType().GetScript();
        }

        public abstract TResult Handle(TArguments message);
    }
}