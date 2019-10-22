# Cqrs.Simple
Simple Cqrs using Depenency Injection providers like Castle and Microsoft DI

[![Build status](https://dev.azure.com/donatekartorg/donatekart/_apis/build/status/Cqrs.Simple-CI)](https://dev.azure.com/donatekartorg/donatekart/_build/latest?definitionId=15) [![Join the chat at https://gitter.im/Cqrs-Simple/community](https://badges.gitter.im/Cqrs-Simple/community.svg)](https://gitter.im/Cqrs-Simple/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

### Where can I get it?

Install [Cqrs.Simple.Castle](https://www.nuget.org/packages/Cqrs.Simple.Castle/) which uses Castle windsor Dependency Injection:

```
Install-Package Cqrs.Simple.Castle
```

Install [Cqrs.Simple.MicrosoftDI](https://www.nuget.org/packages/Cqrs.Simple.MicrosoftDI/) which uses Microsoft Built-in Dependency Injection:

```
Install-Package Cqrs.Simple.MicrosoftDI
```

## Usage

```csharp
public IServiceProvider ConfigureServices(IServiceCollection services)
{
  services.AddCqrs(typeof(Startup).Assembly);
}
```
create a session class implementing ISession interface. The below example uses SQL db connection.
```csharp
public class MyDatabaseSession : ISession
{
    public T Run<T>(Func<IDbConnection, T> func)
    {
        //SQL Server
        IDbConnection conn = new SqlConnection(connectionString);

        try
        {
            conn.Open();
            return func(conn);
        }
        finally
        {
            conn.Close();
        }
    }
}
```

###### Query data:
------------------

Create a query class and provide filter fields.
```csharp
public class GetUserQuery : IQuery
{
    public int Id { get; set; }
}
```

Create a handler for the query as shown below
```csharp
public class GetUserQueryHandler : DatabaseQueryHandler<GetUserQuery, UserInfo>
{
    public GetUserQueryHandler(MyDatabaseSession session) : base(session)
    {
    }

    public GetUserQueryHandler(ISession session) : base(session)
    {
    }

    public override UserInfo Handle(GetUserQuery message)
    {
        return Session.Run(conn => conn.Query<UserInfo>(Script, message));
    }
}
```

Create a class which holds the result of the query. This example users [Dapper library](https://github.com/StackExchange/Dapper) for mapping query results to c# object.
```csharp
public class UserInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
```

Then you can get the user info by using IExecute interface as shown below.
```csharp
public class UserService
{
    private readonly IExecute execute;

    public UserService(IExecute execute)
    {
        this.execute = execute;
    }

    public UserInfo GetuserInfo(int id)
    {
        return execute.Query<GetUserQuery, UserInfo>(new GetUserQuery
        {
            Id = id //pass your criteria here. you can have multiple fields and they will match variables in the sql prefixing symbol @
        });
    }
}
```

Add an embedded resource of your SQL query with the same name as your query class **_GetUserQuery.sql_**
```sql
SLECT Id,
	Name,
	Email,
	Phone
FROM [dbo].[Users]
WHERE [Id] = @Id
```

The sample uses these namespaces
```csharp
using System;
using Dapper;
using System.Data;
```

You can install [Cqrs.Simple](https://www.nuget.org/packages/Cqrs.Simple/) seperatley and use with any other DI container

```
Install-Package Cqrs.Simple
```