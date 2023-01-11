using BlazorYoutube.Web.Database;
using BlazorYoutube.Web.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace BlazorYoutube.Web.Services;

public class TaskService
{
    private readonly DatabaseContext _context;

    public TaskService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<TaskEntity>> GetTasksAsync(string username)
    {
        return await _context.Tasks.Where(x => x.Username == username && !x.IsCompleted).ToListAsync();
    }

    public async Task<TaskEntity?> GetTaskAsync(int id, string username)
    {
        var task = await _context.Tasks.SingleOrDefaultAsync(x => x.Username == username && x.Id == id);
        if (task == null)
        {
            return null;
        }

        return task;
    }

    public async Task<TaskEntity> AddTaskAsync(TaskEntity task, string username)
    {
        task.CreatedDate = DateTime.Now;
        task.Username = username;
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task MarkAsCompletedAsync(int id, string username)
    {
        var task = await _context.Tasks.SingleOrDefaultAsync(x => x.Username == username && x.Id == id);
        if (task == null)
        {
            return;
        }

        task.IsCompleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id, string username)
    {
        var task = await _context.Tasks.SingleOrDefaultAsync(x => x.Username == username && x.Id == id);
        if (task == null)
        {
            return;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}