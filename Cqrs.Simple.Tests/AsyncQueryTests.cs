using System.Threading.Tasks;
using Castle.Windsor;
using Cqrs.Simple.Castle.Installers;
using Xunit;

namespace Cqrs.Simple.Tests
{
    public class AsyncQueryTests
    {
        [Fact]
        public async Task TestExecuteAsyncQuery()
        {
            var container = new WindsorContainer();
            container.Install(new CoreInstaller());
            container.Install(new CqrsInstaller(typeof(AsyncQueryTests).Assembly));

            IExecute execute = container.Kernel.Resolve<IExecute>();

            var result = await execute.QueryAsync<TestAsyncQuery, bool>(new TestAsyncQuery());

            Assert.True(result);
        }
    }

    public class TestAsyncQuery : IQuery
    { }

    public class TestAsyncQueryHandler : IHandleQueryAsync<TestAsyncQuery, bool>
    {
        public async Task<bool> Handle(TestAsyncQuery message)
        {
            return await Task.FromResult(true);
        }
    }
}
