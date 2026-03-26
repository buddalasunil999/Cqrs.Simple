using System.Threading.Tasks;

namespace Cqrs.Simple.Tests
{
    public class TestWithReturnValueCommand : ICommand
    { }

    public class TestWithReturnValueCommandHandler : IHandleCommand<TestWithReturnValueCommand, int>
    {
        public Task<int> Handle(TestWithReturnValueCommand message)
        {
            return Task.FromResult(6);
        }
    }
}
