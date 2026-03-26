namespace Cqrs.Simple.Tests
{
    public class TestSyncQuery : IQuery<int>
    { }

    public class TestSyncQueryHandler : IHandleQuery<TestSyncQuery, int>
    {
        public int Handle(TestSyncQuery message)
        {
            return 5;
        }
    }
}
