using System.Threading.Tasks;

namespace Cqrs.Simple.Tests
{
    public class TestWithGenericReturnCommand : ICommand<int>
    {
    }

    public class TestWithGenericReturnCommandHandler : IHandleCommand<TestWithGenericReturnCommand, int>
    {
        public Task<int> Handle(TestWithGenericReturnCommand message)
        {
            return Task.FromResult(6);
        }
    }
}
