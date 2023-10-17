using TaskManagement.Core.Aggregates;

namespace TaskManagement.Core.Interfaces.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<List<Project>> ListAsync(CancellationToken cancellationToken);

    Task AddAsync(Project entity, CancellationToken cancellationToken);

    Task UpdateAsync(Project entity, CancellationToken cancellationToken);

    Task DeleteAsync(Project entity, CancellationToken cancellationToken);

    Task<List<Project>> SearchByNameAsync(string name, CancellationToken cancellationToken);
}