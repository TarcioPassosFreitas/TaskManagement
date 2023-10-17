using Ardalis.Result;
using TaskManagement.Core.Aggregates;
using TaskManagement.Core.Interfaces.Repositories;
using TaskManagement.Core.Interfaces.Services;

namespace TaskManagement.Core.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<Project>> AddAsync(Project entity, CancellationToken cancellationToken)
    {
        if (entity == null)
        {
            return Result.NotFound("A entidade não pode ser nula");
        }

        await _projectRepository.AddAsync(entity, cancellationToken);
        return Result.Success(entity);
    }

    public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(id, cancellationToken);

        if (project == null)
        {
            return Result.NotFound("Projeto não encontrado");
        }

        await _projectRepository.DeleteAsync(project, cancellationToken);
        return Result.Success(true);
    }

    public async Task<Result<Project>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(id, cancellationToken);

        if (project == null)
        {
            return Result.NotFound("Projeto não encontrado");
        }

        return Result.Success(project);
    }

    public async Task<Result<List<Project>>> ListAsync(CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.ListAsync(cancellationToken);

        if (projects == null || projects.Count == 0)
        {
            return Result.NotFound("Nenhum projeto encontrado");
        }

        return Result.Success(projects);
    }

    public async Task<Result<List<Project>>> SearchByNameAsync(string name, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.SearchByNameAsync(name, cancellationToken);

        if (projects == null || projects.Count == 0)
        {
            return Result.NotFound($"Nenhum projeto encontrado com o nome: {name}");
        }

        return Result.Success(projects);
    }

    public async Task<Result<Project>> UpdateAsync(Project entity, CancellationToken cancellationToken)
    {
        if (entity == null)
        {
            return Result.NotFound("A entidade não pode ser nula");
        }

        var existingProject = await _projectRepository.GetByIdAsync(entity.Id, cancellationToken);

        if (existingProject == null)
        {
            return Result.NotFound("Projeto não encontrado");
        }

        existingProject.Name = entity.Name;
        existingProject.Description = entity.Description;

        await _projectRepository.UpdateAsync(existingProject, cancellationToken);
        return Result.Success(existingProject);
    }
}