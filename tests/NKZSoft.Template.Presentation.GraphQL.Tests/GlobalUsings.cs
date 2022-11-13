global using System.Globalization;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using DotNet.Testcontainers.Containers;
global using FluentAssertions;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore;
global using Xunit;
global using Xunit.Extensions.Ordering;

global using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;
global using NKZSoft.Template.Persistence.PostgreSQL;
global using NKZSoft.Template.Common.Tests;
global using NKZSoft.Template.Application.Common.Interfaces;
global using RestSharp;
