using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Cqrs.Simple.Tests
{
    public class CqrsMicrosoftDITests
    {
        private readonly IExecute execute;

        public CqrsMicrosoftDITests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCqrs(typeof(CqrsMicrosoftDITests).Assembly);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            execute = serviceProvider.GetService<IExecute>();
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
