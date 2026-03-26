using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cqrs.Simple.MicrosoftDI
{
    public class Execute : ExecuteBase
    {
        private readonly IServiceProvider serviceProvider;

        public Execute(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override object Resolve(Type type)
        {
            return serviceProvider.GetService(type);
        }

        protected override T Resolve<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}