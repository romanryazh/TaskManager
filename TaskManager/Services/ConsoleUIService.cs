using System.Threading.Channels;
using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.Services;

/// <summary>
/// Сервис консольного меню менеджера задач для отображения элементов управления.
/// </summary>
public class ConsoleUIService
{
    /// <summary>
    /// Опции меню.
    /// </summary>
    private enum MenuItem
    {
        ShowTasks = 1,
        AddTask,
        EditTask,
        DeleteTask,
        Exit
    }

    private readonly ITaskService _taskService;
    private readonly IFileService _fileService;

    private readonly Dictionary<MenuItem, Action> _menuActions;

    private bool _isRunning = false;

    public ConsoleUIService(ITaskService taskService, IFileService fileService)
    {
        _taskService = taskService;
        _fileService = fileService;

        _menuActions = new Dictionary<MenuItem, Action>
        {
            [MenuItem.ShowTasks] = ShowTasks,
            [MenuItem.AddTask] = AddTask,
            [MenuItem.EditTask] = EditTask,
            [MenuItem.DeleteTask] = DeleteTask,
            [MenuItem.Exit] = Exit,
        };

        _isRunning = true;
    }

    /// <summary>
    /// Запускает и обрабатывает главный цикл сервиса.
    /// Отображает пункты меню, ожидает ввод выбора пользователя,
    /// проверяет корректность ввода, производит выбранные действия.
    /// </summary>
    public void ShowMenu()
    {
        while (_isRunning)
        {
            Console.Clear();
            Console.WriteLine("1. Показать все задачи");
            Console.WriteLine("2. Добавить задачу");
            Console.WriteLine("3. Изменить задачу");
            Console.WriteLine("4. Удалить задачу");
            Console.WriteLine("5. Сохранить и выйти");

            var input = Console.ReadLine();
            Console.Clear();
            if (string.IsNullOrEmpty(input))
            {
                ErrorHandler.ShowError(ErrorType.InputEmpty);
                Console.ReadKey();
                continue;
            }

            if (Enum.TryParse(input, out MenuItem menuItem))
            {
                if (_menuActions.TryGetValue(menuItem, out var action))
                {
                    action();
                }
                else
                {
                    ErrorHandler.ShowError(ErrorType.MenuInvalidItem);
                    Console.ReadKey();
                }
            }
        }
    }

    private void ShowOptions()
    {
        
    }

    /// <summary>
    /// Выводит на экран пронумерованный список всех сохранённых задач.
    /// </summary>
    private void ShowTasks()
    {
        var tasks = _taskService.GetAllTasks();

        foreach (var task in tasks.Select((v, i) => new { value = v, index = i + 1 }))
        {
            Console.WriteLine($"{task.index}. {task.value.Title}");
        }

        Console.ReadKey();
    }

    /// <summary>
    /// Добавляет новую задачу.
    /// Ожидает ввода информации для задачи, проверяет корректность ввода, а затем - добавляет задачу.
    /// </summary>
    private void AddTask()
    {
        Console.WriteLine("Опишите задачу:");

        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            ErrorHandler.ShowError(ErrorType.InputEmpty);
            Console.ReadKey();
            return;
        }

        _taskService.AddTask(new TaskItem() { Title = input });
        NotificationHandler.ShowMessage(NotificationType.TaskAdded);
        Console.ReadKey();
    }

    /// <summary>
    /// Удаляет задачу.
    /// Ожидает ввода номера задачи, проверяет корректность ввода, а затем, удаляет задачу.
    /// </summary>
    private void DeleteTask()
    {
        Console.WriteLine("Укажите номер удаляемой задачи");

        var input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            ErrorHandler.ShowError(ErrorType.InputEmpty);
            Console.ReadKey();
            return;
        }

        if (!int.TryParse(input, out int taskNumber))
        {
            ErrorHandler.ShowError(ErrorType.MenuInvalidItem);
            Console.ReadKey();
            return;
        }

        if (taskNumber < 1 || taskNumber > _taskService.GetAllTasks().Count())
        {
            ErrorHandler.ShowError(ErrorType.TaskOutRange);
            Console.ReadKey();
            return;
        }

        _taskService.GetAllTasks().RemoveAt(taskNumber - 1);
        NotificationHandler.ShowMessage(NotificationType.TaskRemoved);
        Console.ReadKey();
    }

    /// <summary>
    /// Редактирует задачу.
    /// Ожидает ввода номера задачи, проверяет корректность ввода,
    /// ожидает ввода новой информации, а затем, применяет изменения.
    /// </summary>
    private void EditTask()
    {
        Console.WriteLine("Укажите номер изменяемой задачи");

        var input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            ErrorHandler.ShowError(ErrorType.InputEmpty);
            Console.ReadKey();
            return;
        }

        if (!int.TryParse(input, out int taskNumber))
        {
            ErrorHandler.ShowError(ErrorType.MenuInvalidItem);
            Console.ReadKey();
            return;
        }

        if (taskNumber < 1 || taskNumber > _taskService.GetAllTasks().Count())
        {
            ErrorHandler.ShowError(ErrorType.TaskOutRange);
            Console.ReadKey();
            return;
        }
        Console.Clear();
        
        var title = _taskService.GetAllTasks()[taskNumber - 1].Title;
        Console.WriteLine("Задача:");
        Console.WriteLine(title);
        Console.WriteLine("Введите новое описание задачи:");

        input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            ErrorHandler.ShowError(ErrorType.InputEmpty);
            Console.ReadKey();
            return;
        }

        _taskService.GetAllTasks()[taskNumber - 1].Title = input;
        NotificationHandler.ShowMessage(NotificationType.TaskUpdated);
        Console.ReadKey();
    }

    /// <summary>
    /// Прерывает работу сервиса и сохраняет задачи.
    /// </summary>
    private void Exit()

    {
        _isRunning = false;
        _fileService.SaveTasks(_taskService.GetAllTasks());
    }

    private void WaitKey() => Console.ReadKey();
}