using Ardalis.Result;
using TaskManagement.Core.Aggregates;

namespace TaskManagement.Core.Interfaces.Services;

public interface ITaskItemService
{
    Task<Result<TaskItem>> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<Result<List<TaskItem>>> ListAsync(CancellationToken cancellationToken);

    Task<Result<TaskItem>> AddAsync(TaskItem entity, CancellationToken cancellationToken);

    Task<Result<TaskItem>> UpdateAsync(TaskItem entity, CancellationToken cancellationToken);

    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken);

    Task<Result<List<TaskItem>>> SearchByNameAsync(string name, CancellationToken cancellationToken);
}