using System.Threading.Tasks;

namespace Cqrs.Simple
{
    public abstract class DatabaseCommandHandler<TArguments> : IHandleCommand<TArguments>
        where TArguments : ICommand
    {
        protected readonly string Script;
        protected readonly ISession Session;

        protected DatabaseCommandHandler(ISession session)
        {
            Session = session;
            Script = GetType().GetScript();
        }

        public abstract Task Handle(TArguments message);
    }
}