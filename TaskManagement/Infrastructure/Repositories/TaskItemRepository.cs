using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Aggregates;
using TaskManagement.Core.Interfaces.Repositories;
using TaskManagement.Infrastructure.Database.Contexts;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly DatabaseContext _databaseContext;

    public TaskItemRepository(DatabaseContext dbContext)
    {
        _databaseContext = dbContext;
    }

    public async Task AddAsync(TaskItem entity, CancellationToken cancellationToken)
    {
        await _databaseContext.TaskItems.AddAsync(entity, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TaskItem entity, CancellationToken cancellationToken)
    {
        _databaseContext.TaskItems.Remove(entity);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _databaseContext.TaskItems
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<TaskItem>> ListAsync(CancellationToken cancellationToken)
    {
        return await _databaseContext.TaskItems.ToListAsync(cancellationToken);
    }

    public async Task<List<TaskItem>> SearchByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _databaseContext.TaskItems
            .Where(t => t.Name.Contains(name))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(TaskItem entity, CancellationToken cancellationToken)
    {
        _databaseContext.TaskItems.Update(entity);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}