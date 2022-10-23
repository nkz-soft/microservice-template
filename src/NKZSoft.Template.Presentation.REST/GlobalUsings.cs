global using System;
global using System.Net;
global using System.Reflection;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc.Filters;

global using MediatR;
global using FluentResults;
global using NKZSoft.Template.Application.Common.Paging;
global using NKZSoft.Template.Application.Models;
global using NKZSoft.Template.Application.TodoItems.Commands.Create;
global using NKZSoft.Template.Application.TodoItems.Models;
global using NKZSoft.Template.Application.TodoItems.Queries.Get;
global using NKZSoft.Template.Application.TodoItems.Queries.GetPage;
global using NKZSoft.Template.Common;
global  using Serilog;


