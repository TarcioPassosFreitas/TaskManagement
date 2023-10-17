namespace TaskManagement.Core.Aggregates;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public List<TaskItem> TaskItems { get; set; } = new();
}
