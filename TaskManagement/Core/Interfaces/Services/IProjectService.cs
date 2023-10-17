using Ardalis.Result;
using TaskManagement.Core.Aggregates;

namespace TaskManagement.Core.Interfaces.Services;

public interface IProjectService
{
    Task<Result<Project>> AddAsync(Project entity, CancellationToken cancellationToken);

    Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken);

    Task<Result<Project>> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<Result<List<Project>>> ListAsync(CancellationToken cancellationToken);

    Task<Result<List<Project>>> SearchByNameAsync(string name, CancellationToken cancellationToken);

    Task<Result<Project>> UpdateAsync(Project entity, CancellationToken cancellationToken);
}