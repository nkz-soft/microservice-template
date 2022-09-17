using System;
using NKZSoft.Template.Application.Common.Interfaces;

namespace NKZSoft.Template.Infrastructure.Core.Services;

public sealed class MachineDateTime : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}