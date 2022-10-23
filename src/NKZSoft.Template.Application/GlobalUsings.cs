global using System.Collections.Generic;
global using System.Reflection;

global using Microsoft.Extensions.Logging;
global using Microsoft.EntityFrameworkCore;
global using FluentValidation;
global using FluentResults;
global using MediatR;
global using Mapster;
global using MapsterMapper;
global using System.Linq.Expressions;
global using Ardalis.Specification;
global using Ardalis.Specification.EntityFrameworkCore;
global using MassTransit;
global using NKZSoft.Template.Common;

global using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;
global using NKZSoft.Template.Domain.Events;
global using NKZSoft.Template.Events.Event.ToDoItem.Create;
global using NKZSoft.Template.Events.Event.ToDoItem.Update;
