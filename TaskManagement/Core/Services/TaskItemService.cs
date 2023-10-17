using Ardalis.Result;
using TaskManagement.Core.Aggregates;
using TaskManagement.Core.Interfaces.Repositories;
using TaskManagement.Core.Interfaces.Services;

namespace TaskManagement.Core.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;

    public TaskItemService(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public async Task<Result<TaskItem>> AddAsync(TaskItem entity, CancellationToken cancellationToken)
    {
        if (entity == null)
        {
            return Result.NotFound("A entidade não pode ser nula");
        }

        await _taskItemRepository.AddAsync(entity, cancellationToken);
        return Result.Success(entity);
    }

    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var taskItem = await _taskItemRepository.GetByIdAsync(id, cancellationToken);

        if (taskItem == null)
        {
            return Result.NotFound("Tarefa não encontrada");
        }

        await _taskItemRepository.DeleteAsync(taskItem, cancellationToken);
        return Result.Success(true);
    }

    public async Task<Result<TaskItem>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var taskItem = await _taskItemRepository.GetByIdAsync(id, cancellationToken);

        if (taskItem == null)
        {
            return Result.NotFound("Tarefa não encontrada");
        }

        return Result.Success(taskItem);
    }

    public async Task<Result<List<TaskItem>>> ListAsync(CancellationToken cancellationToken)
    {
        var taskItems = await _taskItemRepository.ListAsync(cancellationToken);

        if (taskItems == null || taskItems.Count == 0)
        {
            return Result.NotFound("Nenhuma tarefa encontrada");
        }

        return Result.Success(taskItems);
    }

    public async Task<Result<List<TaskItem>>> SearchByNameAsync(string name, CancellationToken cancellationToken)
    {
        var taskItems = await _taskItemRepository.SearchByNameAsync(name, cancellationToken);

        if (taskItems == null || taskItems.Count == 0)
        {
            return Result.NotFound($"Nenhuma tarefa encontrada com o nome: {name}");
        }

        return Result.Success(taskItems);
    }

    public async Task<Result<TaskItem>> UpdateAsync(TaskItem entity, CancellationToken cancellationToken)
    {
        if (entity == null)
        {
            return Result.NotFound("A entidade não pode ser nula");
        }

        var existingTaskItem = await _taskItemRepository.GetByIdAsync(entity.Id, cancellationToken);

        if (existingTaskItem == null)
        {
            return Result.NotFound("Tarefa não encontrada");
        }

        existingTaskItem.Name = entity.Name;
        existingTaskItem.Description = entity.Description;
        existingTaskItem.IsCompleted = entity.IsCompleted;

        await _taskItemRepository.UpdateAsync(existingTaskItem, cancellationToken);
        return Result.Success(existingTaskItem);
    }
}