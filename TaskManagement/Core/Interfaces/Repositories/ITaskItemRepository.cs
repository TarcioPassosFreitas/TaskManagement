using TaskManagement.Core.Aggregates;

namespace TaskManagement.Core.Interfaces.Repositories;

public interface ITaskItemRepository
{
    Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<List<TaskItem>> ListAsync(CancellationToken cancellationToken);

    Task AddAsync(TaskItem entity, CancellationToken cancellationToken);

    Task UpdateAsync(TaskItem entity, CancellationToken cancellationToken);

    Task DeleteAsync(TaskItem entity, CancellationToken cancellationToken);

    Task<List<TaskItem>> SearchByNameAsync(string name, CancellationToken cancellationToken);
}