global using System;
global using System.ServiceModel;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Linq;
global using System.Runtime.CompilerServices;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;

global using Mapster;
global using MapsterMapper;
global using FluentResults;
global using ProtoBuf;
global using ProtoBuf.Grpc.Server;
global using MediatR;

global using NKZSoft.Template.Application.Common.Paging;
global using NKZSoft.Template.Application.TodoItems.Models;
global using NKZSoft.Template.Application.TodoItems.Queries.Get;
global using NKZSoft.Template.Application.TodoItems.Queries.GetPage;

