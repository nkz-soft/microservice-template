namespace NKZSoft.Template.Application.Common.Repositories.Redis;

public interface IToDoItemRedisRepository
{
    public Task AddAsync(ToDoItem entity, CancellationToken cancellationToken = default);

    public Task<ToDoItem?> GetAsyncByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task DeleteAsync(ToDoItem entity, CancellationToken cancellationToken = default);

    public Task DeleteAllAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
