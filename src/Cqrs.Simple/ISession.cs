using System;
using System.Data;

namespace Cqrs.Simple
{
    public interface ISession
    {
        T Run<T>(Func<IDbConnection, T> func);
        T Run<T>(Func<IDbConnection, IDbTransaction, T> func);
    }
}