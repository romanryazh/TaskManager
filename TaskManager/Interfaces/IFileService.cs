using TaskManager.Models;

namespace TaskManager.Interfaces;

public interface IFileService
{
    void SaveTasks(List<TaskItem> tasks, string filePath = "tasks.json");
    List<TaskItem> LoadTasks(string filePath = "tasks.json");
}