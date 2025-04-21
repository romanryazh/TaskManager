using System.Text.Json;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Services;

public class FileService : IFileService
{
    private const string FilePath = "tasks.json";

    public void SaveTasks(List<TaskItem> tasks)
    {
        var json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(FilePath, json);
    }

    public List<TaskItem> LoadTasks()
    {
        if (!File.Exists(FilePath)) return new List<TaskItem>();
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<TaskItem>>(json);
    }
}