global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.DependencyInjection;

global using ProtoBuf.Grpc.Client;
global using Microsoft.EntityFrameworkCore;
global using Grpc.Net.Client;
global using Xunit;
global using Xunit.Abstractions;
global using Xunit.Extensions.Ordering;
global using FluentAssertions;

global using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;
global using NKZSoft.Template.Persistence.PostgreSQL;
global using NKZSoft.Template.Application.Common.Interfaces;
global using NKZSoft.Template.Common.Tests;
global using NKZSoft.Template.Presentation.GRPC.Services;
global using NKZSoft.Template.Presentation.GRPC.Models;
global using NKZSoft.Template.Presentation.GRPC.Tests.Common;
