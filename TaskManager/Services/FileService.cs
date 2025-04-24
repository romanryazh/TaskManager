using System.Text.Json;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Services;

public class FileService : IFileService
{
    private const string FilePath = "tasks.json";

    public void SaveTasks(List<TaskItem> tasks, string filePath = "tasks.json")
    {
        var json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(filePath, json);
    }

    public List<TaskItem> LoadTasks(string filePath = "tasks.json")
    {
        if (!File.Exists(filePath)) return new List<TaskItem>();
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<TaskItem>>(json);
    }
}