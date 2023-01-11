namespace BlazorYoutube.Contracts;

public class ListOfTasksDto
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public IEnumerable<TaskDto> Tasks { get; set; }
}