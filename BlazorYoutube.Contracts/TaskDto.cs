namespace BlazorYoutube.Contracts;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; } = DateTime.Now;
    public DateTime? CreatedDate { get; set; }
    public bool IsCompleted { get; set; }
}