# microservice-template
Template for microservice based on DDD and Clean Architecture with .NET

The main aim of this project is to provide a means for building microservices with the latest technology and architecture.

# Installing as a template

```bash
dotnet new install  .\ 
```

Creating a new application

```bash
dotnet new nkz-template -o My.NewApp
```

## ⭐ Give a star

If you're using this repository for your learning, samples or your project, please give a star. Thanks :+1:

## Table of Contents

- [The aim of the project](#the-aim-of-this-project)
- [Plan](#plan)
- [Technologies - Libraries](#technologies-used)

## The aim of the project

I would like this project to help you find simplified and effortless solutions. You can treat it as a modular project and reuse the modules in your projects.

## Plan

- [x] REST API samples
- [x] GRPC API samples
- [x] Improve integration tests to use Testcontainers
- [x] GraphQL API samples
- [x] MassTransit and RabbitMq
- [x] Minimal hosting model
- [x] Migrate to .NET 7
- [x] HealthCheck
- [x] Websocket (SignalR)
- [x] GRPC Error handling in interceptors
- [ ] OpenTelemetry
- [ ] Add caching for EF Core
- [ ] MongoDB data provider

## Technologies used

[NET Core 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) : .NET Core, including ASP.NET Core

[EntityFrameworkCore](https://github.com/dotnet/efcore): EF Core is a modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations.

[Npgsql](https://github.com/npgsql/npgsql): Npgsql is the open source .NET data provider for PostgreSQL. It allows you to connect and interact with PostgreSQL server using .NET.

[Mapster](https://github.com/MapsterMapper/Mapster): A mapper library for .NET.

[GuardClauses](https://github.com/ardalis/GuardClauses): A simple extensible package with guard clause extensions.

[Testcontainers](https://github.com/testcontainers/testcontainers-dotnet): Testcontainers is a library to support tests with throwaway instances of Docker containers for all compatible .NET Standard versions.

[FluentValidation](https://github.com/FluentValidation/FluentValidation): A validation library for .NET that uses a fluent interface and lambda expressions for building strongly-typed validation rules.

[FluentAssertions](https://github.com/fluentassertions/fluentassertions): A very extensive set of extension methods that allow you to more naturally specify the expected outcome of a TDD or BDD-style unit tests.

[protobuf-net](https://github.com/protobuf-net/protobuf-net): protobuf-net is a contract based serializer for .NET code, that happens to write data in the "protocol buffers" serialization format engineered by Google.

[hotchocolate](https://github.com/ChilliCream/hotchocolate): A GraphQL server to create GraphQL endpoints and merge schemas.

[MassTransit](https://github.com/MassTransit/MassTransit): MassTransit is a free, open-source distributed application framework for .NET.

[FluentResults](https://github.com/altmann/FluentResults): FluentResults is a lightweight .NET library developed to solve a common problem. It returns an object indicating success or failure of an operation instead of throwing/using exceptions.

[Specification](https://github.com/ardalis/Specification): Base class with tests for adding specifications to a DDD model. Also includes a default generic Repository base class with support for EF6 and EF Core.

[Scrutor](https://github.com/khellang/Scrutor): Assembly scanning and decoration extensions for Microsoft.Extensions.DependencyInjection.

[RestSharp](https://github.com/restsharp/RestSharp): RestSharp is a lightweight HTTP client library. It's a wrapper around HttpClient, not a full-fledged client on its own.

[SignalR](https://github.com/dotnet/aspnetcore/tree/main/src/SignalR): ASP.NET Core SignalR is a library for ASP.NET Core developers that makes it incredibly simple to add real-time web functionality to your applications.
