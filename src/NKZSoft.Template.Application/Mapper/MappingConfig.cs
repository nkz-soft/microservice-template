using NKZSoft.Template.Application.Models;
using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

namespace NKZSoft.Template.Application.Mapper;

using Models;

public class MappingConfig
{
    public static TypeAdapterConfig Configure()
    {
        var config = new TypeAdapterConfig();

        config.NewConfig<ToDoItem, ToDoItemDto>();

        config.NewConfig<ToDoList, ToDoListDto>();

        return config;
    }
}
