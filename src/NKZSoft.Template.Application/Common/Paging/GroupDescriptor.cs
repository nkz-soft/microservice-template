﻿namespace NKZSoft.Template.Application.Common.Paging;

public sealed class GroupDescriptor
{
    public GroupDescriptor(
        string field, EnumSortDirection direction = EnumSortDirection.Asc, bool isExpanded = false)
    {
        Field = field;
        Direction = direction;
        IsExpanded = isExpanded;
    }

    public string Field { get; }

    public EnumSortDirection Direction { get; }

    public bool IsExpanded { get; }

    public override string ToString()
        => $"{Field}:{Direction}-{IsExpanded}";
}
