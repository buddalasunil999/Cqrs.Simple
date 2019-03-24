using System;
using System.Data;
using System.Threading.Tasks;
using Castle.Windsor;
using Cqrs.Simple.Castle.Installers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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

        [Fact]
        public void TestAddCqrs()
        {
            var serviceCollection = new Mock<IServiceCollection>();
            serviceCollection.Object.AddCqrs(typeof(AsyncQueryTests).Assembly);
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

    public abstract class TestSession : ISession
    {
        public T Run<T>(Func<IDbConnection, T> action)
        {
            return Test<T>();
        }

        protected abstract T Test<T>();
    }

    public class FullSession : TestSession
    {
        protected override T Test<T>()
        {
            return default(T);
        }
    }
}
