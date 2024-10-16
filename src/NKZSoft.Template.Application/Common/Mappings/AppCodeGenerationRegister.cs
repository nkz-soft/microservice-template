namespace NKZSoft.Template.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations.Schema;

public class AppCodeGenerationRegister : ICodeGenerationRegister
{
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
