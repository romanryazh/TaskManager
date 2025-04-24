using FluentAssertions;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Tests;

public class FileServiceTests
{
    private const string TestFilePath = "test_tasks.json";
    
    [Fact]
    public void FileService_ShouldSaveAndLoadTasksCorrectly()
    {
        var fileService = new FileService();
        var testTasks = new List<TaskItem>()
        {
            new() { Id = Guid.NewGuid(), Title = "Тестовая задача" }
        };
        
        fileService.SaveTasks(testTasks, TestFilePath);
        
        var loadedTasks = fileService.LoadTasks(TestFilePath);
        loadedTasks.Should().BeEquivalentTo(testTasks);
        
        File.Delete(TestFilePath);
    }

    [Fact]
    public void FileService_ShouldReturnEmptyListIfFileDoesNotExist()
    {
        var fileService = new FileService();
        var loadedTasks = fileService.LoadTasks(TestFilePath);
        loadedTasks.Should().BeEmpty();
    }
    
}