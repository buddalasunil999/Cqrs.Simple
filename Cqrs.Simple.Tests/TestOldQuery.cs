namespace Cqrs.Simple.Tests
{
    public class TestOldQuery : IQuery
    { }

    public class TestOldQueryHandler : IHandleQuery<TestOldQuery, int>
    {
        public int Handle(TestOldQuery message)
        {
            return 5;
        }
    }
}
