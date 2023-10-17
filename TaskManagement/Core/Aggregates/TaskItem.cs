namespace TaskManagement.Core.Aggregates;

public class TaskItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsCompleted { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}