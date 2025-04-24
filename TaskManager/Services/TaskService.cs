using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService : ITaskService
{
    private readonly List<TaskItem> _tasks;
    private readonly IFileService _fileService;

    public TaskService(IFileService fileService)
    {
        _fileService = fileService;
        _tasks = _fileService.LoadTasks();
    }

    public void AddTask(TaskItem task) => _tasks.Add(task);

    public void DeleteTask(Guid taskId)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null) _tasks.Remove(task);
    }

    public void UpdateTask(Guid taskId, string? title)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null) task.Title = title;
    }

    public List<TaskItem> GetAllTasks() => _tasks;
}