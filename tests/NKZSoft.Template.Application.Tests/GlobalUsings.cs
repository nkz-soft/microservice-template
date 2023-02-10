global using Xunit;
global using MediatR;
global using Moq;
global using FluentAssertions;

global using NKZSoft.Template.Application.Common.Paging;
global using NKZSoft.Template.Application.Common.Repositories;
global using NKZSoft.Template.Application.Tests.Common;
global using NKZSoft.Template.Application.TodoItems.Commands.Create;
global using NKZSoft.Template.Application.TodoItems.Commands.Delete;
global using NKZSoft.Template.Application.TodoItems.Models;
global using NKZSoft.Template.Application.TodoItems.Queries.GetPage;
global using NKZSoft.Template.Persistence.PostgreSQL.Repositories;
