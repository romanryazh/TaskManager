using TaskManager.Models;

namespace TaskManager.Interfaces;

public interface ITaskService
{
    void AddTask(TaskItem task);
    void DeleteTask(Guid taskId);
    void UpdateTask(Guid taskId, string title);
    List<TaskItem> GetAllTasks();
}