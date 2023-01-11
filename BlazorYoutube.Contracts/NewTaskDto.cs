﻿namespace BlazorYoutube.Contracts;

public class NewTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; } = DateTime.Now;
}