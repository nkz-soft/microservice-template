namespace NKZSoft.Template.Application.Mapper;

using Events.ToDoItem.Create;
using Models;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ToDoItem, ToDoItemDto>();
        config.NewConfig<ToDoList, ToDoListDto>();

        config.NewConfig<ToDoItemCreatedDomainEvent, ToDoItemCreatedIntegrationEvent>();
    }
}
