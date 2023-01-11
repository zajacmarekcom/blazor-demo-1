using System.Security.Claims;
using BlazorYoutube.Contracts;
using BlazorYoutube.Web.Database.Entities;
using BlazorYoutube.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorYoutube.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TodoController : Controller
{
    private readonly TaskService _taskService;

    public TodoController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var user = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var task = await _taskService.GetTaskAsync(id, user);

        if (task is null)
            return NotFound();

        var dto = new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted
        };

        return Ok(dto);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var user = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var tasks = await _taskService.GetTasksAsync(user);

        var dtos = tasks.Select(task => new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            DueDate = task.DueDate,
            CreatedDate = task.CreatedDate
        }).ToList();

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewTaskDto data)
    {
        var user = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var task = new TaskEntity
        {
            Title = data.Title,
            Description = data.Description,
            DueDate = data.DueDate
        };

        await _taskService.AddTaskAsync(task, user);

        var dto = new TaskDto()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            CreatedDate = task.CreatedDate
        };

        return Ok(dto);
    }

    [HttpPut("complete/{id}")]
    public async Task MarkAsCompleted([FromRoute] int id)
    {
        var user = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _taskService.MarkAsCompletedAsync(id, user);
    }

    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] int id)
    {
        var user = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _taskService.DeleteTaskAsync(id, user);
    }
}