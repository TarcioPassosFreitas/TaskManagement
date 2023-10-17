using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManagement.Core.Aggregates;

namespace TaskManagement.Infrastructure.Database.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) 
            : base(options) { }

        public DbSet<TaskItem> TaskItems { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
