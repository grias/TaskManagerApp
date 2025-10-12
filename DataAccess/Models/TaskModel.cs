namespace DataAccess.Models;

public class TaskModel
{
    public int Id { get; set; }
    public required string Title { get; set; } 
    public required string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}
