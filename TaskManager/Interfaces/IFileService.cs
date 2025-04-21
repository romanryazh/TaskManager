using TaskManager.Models;

namespace TaskManager.Interfaces;

public interface IFileService
{
    void SaveTasks(List<TaskItem> tasks);
    List<TaskItem> LoadTasks();
}