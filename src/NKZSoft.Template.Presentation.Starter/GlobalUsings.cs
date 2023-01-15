global using System;
global using HealthChecks.UI.Client;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using NKZSoft.Service.Configuration.Logger;
global using NKZSoft.Template.Application;
global using NKZSoft.Template.Application.Common.Interfaces;
global using NKZSoft.Template.Infrastructure.Core;
global using NKZSoft.Template.MessageBrokers.RabbitMq.Extensions;
global using NKZSoft.Template.Persistence.PostgreSQL.Extensions;
global using NKZSoft.Template.Presentation.Rest.Extensions;
//#if (EnableGraphQL)
global using NKZSoft.Template.Presentation.GraphQL.Extensions;
//#endif
//#if (EnableGrpc)
global using NKZSoft.Template.Presentation.Grpc.Extensions;
//#endif
//#if (EnableSignalR)
global using NKZSoft.Template.Presentation.SignalR.Extensions;
//#endif

