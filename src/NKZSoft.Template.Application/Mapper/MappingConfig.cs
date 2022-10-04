using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

namespace NKZSoft.Template.Application.Mapper;

using Models;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ToDoItem, ToDoItemDto>();
        config.NewConfig<ToDoList, ToDoListDto>();
    }
}
