using Microsoft.Extensions.DependencyInjection;
using TaskManager.Interfaces;
using TaskManager.Services;

var services = new ServiceCollection();

services.AddSingleton<ITaskService, TaskService>();
services.AddSingleton<IFileService, FileService>();
services.AddSingleton<ConsoleUIService>();

var serviceProvider = services.BuildServiceProvider();
var taskService = serviceProvider.GetRequiredService<ITaskService>();
var fileService = serviceProvider.GetRequiredService<IFileService>();
var ui = serviceProvider.GetRequiredService<ConsoleUIService>();
//
// var tasks = fileService.LoadTasks();
// tasks.ForEach(task => taskService.AddTask(task));

ui.ShowMenu();

