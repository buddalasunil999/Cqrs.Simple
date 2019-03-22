# Cqrs.Simple
Simple Cqrs using Depenency Injection providers like Castle and Microsoft DI

[![Build status](https://dev.azure.com/donatekartorg/donatekart/_apis/build/status/Cqrs.Simple-CI)](https://dev.azure.com/donatekartorg/donatekart/_build/latest?definitionId=15)

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

You can install [Cqrs.Simple](https://www.nuget.org/packages/Cqrs.Simple/) seperatley and use with any other DI container

```
Install-Package Cqrs.Simple
```
