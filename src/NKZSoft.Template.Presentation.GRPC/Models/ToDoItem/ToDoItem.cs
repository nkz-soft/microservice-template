namespace NKZSoft.Template.Presentation.GRPC.Models.ToDoItem;

[ProtoContract (SkipConstructor = true)]
public sealed record ToDoItem
{
    [ProtoMember(1)]
    public Guid Id { get; init; }

    [ProtoMember(2)]
    public string Title { get; init; } = default!;

    [ProtoMember(3)]
    public string? Note { get; init; }

    [ProtoMember(4)]
    public string CreatedBy { get; init; } = default!;

    [ProtoMember(5)]
    public string Created { get; init; }  = default!;

    [ProtoMember(6)]
    public string? ModifiedBy { get; init; }

    [ProtoMember(7)]
    public DateTime? Modified { get; init; }

    [ProtoMember(8)]
    public DateTime? Deleted { get; init; }
}
