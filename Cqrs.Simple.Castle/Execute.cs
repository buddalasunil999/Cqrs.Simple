using Castle.MicroKernel;
using System;

namespace Cqrs.Simple.Castle
{
    public class Execute : ExecuteBase
    {
        private readonly IKernel kernel;

        public Execute(IKernel kernel)
        {
            this.kernel = kernel;
        }

        protected override object Resolve(Type type)
        {
            return kernel.Resolve(type);
        }

        protected override T Resolve<T>()
        {
            return kernel.Resolve<T>();
        }
    }
}