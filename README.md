# microservice-template

![GitHub release (latest SemVer)](https://img.shields.io/github/v/release/nkz-soft/microservice-template?style=flat-square)
![license](https://img.shields.io/github/license/nkz-soft/microservice-template?style=flat-square)
![GitHub Workflow Status (with branch)](https://img.shields.io/github/actions/workflow/status/nkz-soft/microservice-template/build-by-tag.yaml)

Template for microservice based on DDD and Clean Architecture with .NET

The main aim of this project is to provide a means for building microservices with the latest technology and architecture.
I would like this project to help you find simplified and effortless solutions. You can treat it as a modular project and reuse the modules in your projects.

### ⭐ Give a star

If you're using this repository for your learning, samples or your project, please give a star. Thanks :+1:

## Table of Contents

- [Installation](#installation)
- [Deployment](#deployment)
- [Plan](#plan)
- [Technologies - Libraries](#technologies-used)

## Installation

### Installing as a template

```bash
dotnet new install  .\ 
```

Creating a new application

```bash
dotnet new nkz-template -o My.NewApp
```
Also you can change the contents of the generated application by using the parameters.

```bash
dotnet new nkz-template -o My.NewApp1 --EnableGraphQL false --EnableGrpc false --EnableSignalR false --EnableRedisStorage false 
```

![image](https://raw.githubusercontent.com/nkz-soft/microservice-template/main/.github/images/wizard.png)

## Deployment

[Deploy To Local Kubernetes](./deployment/k8s/README.md)

## Plan

- [x] REST API samples
- [x] GRPC API samples
- [x] Improve integration tests to use Testcontainers
- [x] GraphQL API samples
- [x] MassTransit and RabbitMq
- [x] Minimal hosting model
- [x] Migrate to .NET 8
- [x] HealthCheck
- [x] Websocket (SignalR) 
- [x] GRPC Error handling in interceptors
- [x] Enabling Central Package Management
- [x] Uses Chiseled Ubuntu image
- [x] Adding Layer 2 caching for EF Core
- [x] Creating a Helm chart
- [x] OpenTelemetry
- [x] Redis data provider
- [ ] MongoDB data provider

## Technologies used

[NET Core 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0): .NET Core, including ASP.NET Core

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

[EF Core Second Level Cache Interceptor](https://github.com/VahidN/EFCoreSecondLevelCacheInterceptor): Second level caching is a query cache. The results of EF commands will be stored in the cache, so that the same EF commands will retrieve their data from the cache rather than executing them against the database again.

[EasyCaching](https://github.com/dotnetcore/EasyCaching): EasyCaching is an open source caching library that contains basic usages and some advanced usages of caching which can help us to handle caching more easily!

[Helm](https://github.com/helm/helm): Helm is a tool for managing Charts. Charts are packages of pre-configured Kubernetes resources.

[OpenTelemetry .NET](https://github.com/open-telemetry/opentelemetry-dotnet): The .NET OpenTelemetry client.

[Jaeger](https://github.com/jaegertracing/jaeger): Jaeger, is a distributed tracing platform created by Uber Technologies and donated to Cloud Native Computing Foundation. It can be used for monitoring microservices-based distributed systems.
