using System.Net;
using System.Net.Http.Json;
using BlazorYoutube.Contracts;

namespace BlazorYoutube.Frontend.Services;

public class TaskService
{
    private readonly HttpClient _httpClient;
    public TaskService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task AddTaskAsync(NewTaskDto task)
    {
        var content = JsonContent.Create(task);
        await _httpClient.PostAsync("api/Todo", content);
    }
}