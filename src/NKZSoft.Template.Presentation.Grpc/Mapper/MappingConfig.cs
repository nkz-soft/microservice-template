namespace NKZSoft.Template.Presentation.Grpc.Mapper;

using Models;
using Models.Result;
using Models.ToDoItem;
using ToDoItemDto = Application.Models.ToDoItemDto;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetTodoItemQuery, GetTodoItemRequest>();

        config.NewConfig<Result<CollectionViewModel<ToDoItemDto>>, ToDoItemsResponse>()
            .Map(dest => dest.IsSuccess, src => src.IsSuccess)
            .Map(dest => dest.Errors, src => src.Errors)
            .Map(dest => dest.Items, src => src.Value.Data)
            .IgnoreNonMapped(value:true);

        config.NewConfig<Result<ToDoItemDto>, ToDoItemResponse>()
            .Map(dest => dest.IsSuccess, src => src.IsSuccess)
            .Map(dest => dest.Errors, src => src.Errors)
            .Map(dest => dest.Item, src => src.Value)
            .IgnoreNonMapped(value:true);

        config.NewConfig<ToDoItemDto, ToDoItemDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Note, src => src.Note)
            .Map(dest => dest.CreatedBy, src => src.CreatedBy)
            .Map(dest => dest.Created, src => src.Created)
            .Map(dest => dest.ModifiedBy, src => src.ModifiedBy)
            .Map(dest => dest.Modified, src => src.Modified)
            .Map(dest => dest.Deleted, src => src.Deleted)
            .IgnoreNonMapped(value:true);

        config.NewConfig<IError, ErrorResponse>()
            .MapToConstructor(value:true)
            .ConstructUsing(error => new ErrorResponse(error.Message, TransformErrorCode(error)));
    }

    private static string? TransformErrorCode(IReason error)
    {
        if (error.Metadata?.TryGetValue("ErrorCode", out var errorCode) != null && errorCode != null)
        {
            return errorCode as string;
        }
        return null;
    }
}
