﻿namespace NKZSoft.Template.Presentation.GraphQL.Services;

using Application.TodoItems.Commands.Create;

public sealed class Mutation
{
    [UseMutationConvention]
#pragma warning disable CA1822
    public async Task<Guid> CreateToDoItem([Service] IMediator mediator, string title, int? listId, CancellationToken token) =>
        (await mediator.Send(new CreateToDoItemCommand(title, listId), token)).Value;
#pragma warning restore CA1822
}
