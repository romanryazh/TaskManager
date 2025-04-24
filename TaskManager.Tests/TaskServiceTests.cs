using FluentAssertions;
using Moq;
using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Tests;

public class TaskServiceTests
{
    private ITaskService CreateTaskService(List<TaskItem>? preloadedTasks = null)
    {
        var mockFileService = new Mock<IFileService>();
        mockFileService
            .Setup(x => x.LoadTasks(""))
            .Returns(preloadedTasks ?? []);

        return new TaskService(mockFileService.Object);
    }

    [Fact]
    public void TaskService_ShouldLoadTasksFromFile()
    {
        List<TaskItem> tasks = [new() { Id = Guid.NewGuid(), Title = "Тестовая задача" }];
        var taskService = CreateTaskService(tasks);

        taskService.GetAllTasks().Should().HaveCount(1);
    }

    [Fact]
    public void AddTask_ShouldAddTaskToList()
    {
        var taskService = CreateTaskService();
        var task = new TaskItem()
            { Id = Guid.NewGuid(), Title = "Тестовая задача" };

        taskService.AddTask(task);

        var allTask = taskService.GetAllTasks();
        allTask.Should().Contain(task);
    }

    [Fact]
    public void DeleteTask_ShouldRemoveTaskFromList()
    {
        var taskService = CreateTaskService();
        var task = new TaskItem()
            { Id = Guid.NewGuid(), Title = "Тестовая задача" };
        taskService.AddTask(task);

        taskService.DeleteTask(task.Id);

        var allTask = taskService.GetAllTasks();
        allTask.Should().BeEmpty();
    }

    [Fact]
    public void DeleteTask_ShouldNotThrowIfTaskNotFound()
    {
        var taskService = CreateTaskService();
        var act = () => taskService.DeleteTask(Guid.NewGuid());
        
        act.Should().NotThrow();
    }

    [Fact]
    public void UpdateTask_ShouldUpdateTaskInList()
    {
        var taskService = CreateTaskService();
        var task = new TaskItem()
            { Id = Guid.NewGuid(), Title = "Тестовая задача" };
        taskService.AddTask(task);

        string newTaskTitle = "Изменённая задача";
        taskService.UpdateTask(task.Id, newTaskTitle);

        taskService.GetAllTasks().Should().Contain(t => task.Title == newTaskTitle);
    }

    
}