using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Cqrs.Simple.Castle.Installers;
using Xunit;

namespace Cqrs.Simple.Tests
{
    public class CqrsCastleTests
    {
        private readonly IExecute execute;

        public CqrsCastleTests()
        {
            var container = new WindsorContainer();
            container.Install(Assemblies().ToArray());

            execute = container.Resolve<IExecute>();
        }

        private IEnumerable<IWindsorInstaller> Assemblies()
        {
            yield return new CoreInstaller();
            yield return new CqrsInstaller("Cqrs");
        }

        [Fact]
        public void TestOldQuery()
        {
            var result = execute.Query<TestOldQuery, int>(new TestOldQuery());

            Assert.Equal(5, result);
        }

        [Fact]
        public async Task TestQuery()
        {
            var result = await execute.QueryAsync(new TestQuery());

            Assert.Equal(5, result);
        }

        [Fact]
        public void TestSyncQuery()
        {
            var result = execute.Query(new TestSyncQuery());

            Assert.Equal(5, result);
        }

        [Fact]
        public async Task TestAsyncQuery()
        {
            var result = await execute.QueryAsync<TestAsyncQuery, int>(new TestAsyncQuery());

            Assert.Equal(5, result);
        }

        [Fact]
        public async Task TestCommand()
        {
            var result = execute.Command(new TestCommand());

            await result;
        }

        [Fact]
        public async Task TestCommandWithReturnValue()
        {
            var result = await execute.Command<TestWithReturnValueCommand, int>(new TestWithReturnValueCommand());

            Assert.Equal(6, result);
        }

        [Fact]
        public async Task TestCommandWithGenericReturn()
        {
            var result = await execute.Command(new TestWithGenericReturnCommand());

            Assert.Equal(6, result);
        }
    }
}
