using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Aggregates;

namespace TaskManagement.Infrastructure.Database.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        
        builder.HasMany(x => x.TaskItems)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId);
    }
}