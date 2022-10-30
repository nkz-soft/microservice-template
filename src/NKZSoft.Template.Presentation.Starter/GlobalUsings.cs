global using System;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Builder;

global using NKZSoft.Service.Configuration.Logger;
global using NKZSoft.Template.Application;
global using NKZSoft.Template.Application.Common.Interfaces;
global using NKZSoft.Template.Infrastructure.Core;
global using NKZSoft.Template.MessageBrokers.RabbitMq.Extensions;
global using NKZSoft.Template.Persistence.PostgreSQL;
global using NKZSoft.Template.Presentation.GraphQL.Extensions;
global using NKZSoft.Template.Presentation.GRPC.Extensions;
global using NKZSoft.Template.Presentation.REST.Extensions;
