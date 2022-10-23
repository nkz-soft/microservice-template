using System.ComponentModel.DataAnnotations.Schema;

namespace NKZSoft.Template.Application.Common.Mappings;

public class AppCodeGenerationRegister : ICodeGenerationRegister
{
    //TODO Add async support
    //see https://github.com/MapsterMapper/Mapster/wiki/Async
    public void Register(CodeGenerationConfig config)
    {
        config.AdaptTo("[name]Dto").ForType<ToDoItem>()
            .IgnoreAttributes(typeof(NotMappedAttribute));
        config.AdaptTo("[name]Dto").ForType<ToDoList>()
            .IgnoreAttributes(typeof(NotMappedAttribute));

        config.GenerateMapper("[name]Mapper")
            .ForType<ToDoItem>()
            .ForType<ToDoList>();
    }
}
