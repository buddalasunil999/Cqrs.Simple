using System.Threading.Tasks;

namespace Cqrs.Simple.Tests
{
    public class TestQuery : IQuery<int>
    { }

    public class TestQueryHandler : IHandleQueryAsync<TestQuery, int>
    {
        public Task<int> Handle(TestQuery message)
        {
            return Task.FromResult(5);
        }
    }
}
