namespace TaskManager.Models;

public class TaskItem
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool IsCompleted { get; set; }
    public string Category { get; set; }
    
}