using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Aggregates;
using TaskManagement.Core.Interfaces.Repositories;
using TaskManagement.Infrastructure.Database.Contexts;

namespace TaskManagement.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ProjectRepository(DatabaseContext dbContext)
        {
            _databaseContext = dbContext;
        }

        public async Task AddAsync(Project entity, CancellationToken cancellationToken)
        {
            await _databaseContext.Projects.AddAsync(entity, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Project entity, CancellationToken cancellationToken)
        {
            _databaseContext.Projects.Remove(entity);
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _databaseContext.Projects
                .Include(p => p.TaskItems)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<List<Project>> ListAsync(CancellationToken cancellationToken)
        {
            return await _databaseContext.Projects
                .Include(p => p.TaskItems)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Project>> SearchByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _databaseContext.Projects
                .Where(p => p.Name.Contains(name))
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Project entity, CancellationToken cancellationToken)
        {
            _databaseContext.Projects.Update(entity);
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }
    }
}
