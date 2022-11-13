global using System.Net;
global using DotNet.Testcontainers.Containers;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore;

global using Xunit;
global using Xunit.Extensions.Ordering;
global using FluentAssertions;
global using MediatR;

global using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;
global using NKZSoft.Template.Persistence.PostgreSQL;
global using NKZSoft.Template.Common.Tests;
global using NKZSoft.Template.Application.Models;
global using NKZSoft.Template.Application.Common.Interfaces;
global using NKZSoft.Template.Application.Common.Paging;
global using NKZSoft.Template.Application.TodoItems.Commands.Create;
global using NKZSoft.Template.Application.TodoItems.Models;
global using NKZSoft.Template.Presentation.REST.Models.Result;

