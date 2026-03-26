using System.Threading.Tasks;

namespace Cqrs.Simple.Tests
{
    public class TestCommand : ICommand
    {
    }

    public class TestCommandHandler : IHandleCommand<TestCommand>
    {
        public Task Handle(TestCommand message)
        {
            return Task.CompletedTask;
        }
    }
}
