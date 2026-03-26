using System;

namespace Cqrs.Simple
{
    public interface IStartable : IDisposable
    {
        void Begin();
        void Complete();
    }
}