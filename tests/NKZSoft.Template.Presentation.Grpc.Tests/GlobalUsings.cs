global using DotNet.Testcontainers.Containers;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.DependencyInjection;

global using ProtoBuf.Grpc.Client;
global using Microsoft.EntityFrameworkCore;
global using Grpc.Net.Client;
global using Xunit;
global using FluentAssertions;

global using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;
global using NKZSoft.Template.Persistence.PostgreSQL;
global using NKZSoft.Template.Application.Common.Interfaces;
global using NKZSoft.Template.Common.Tests;
global using NKZSoft.Template.Persistence.PostgreSQL.Extensions;
global using Xunit.Extensions.Ordering;
