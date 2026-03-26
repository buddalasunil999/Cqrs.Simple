using System.Threading.Tasks;

namespace Cqrs.Simple.Tests
{
    public class TestAsyncQuery : IQuery
    { }

    public class TestAsyncQueryHandler : IHandleQueryAsync<TestAsyncQuery, int>
    {
        public async Task<int> Handle(TestAsyncQuery message)
        {
            return await Task.FromResult(5);
        }
    }
}
