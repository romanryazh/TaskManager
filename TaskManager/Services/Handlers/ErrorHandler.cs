namespace TaskManager.Services.Handlers;

public enum ErrorType
{
    InputEmpty,
    MenuInvalidItem,
    TaskOutRange,
}

public static class ErrorHandler
{
    private static readonly Dictionary<ErrorType, string> Messages = new()
    {
        [ErrorType.InputEmpty] = "Ошибка: ввод не может быть пустым",
        [ErrorType.MenuInvalidItem] = "Ошибка: неверный номер",
        [ErrorType.TaskOutRange] = "Ошибка: нет задачи с таким номером"
    };

    public static void ShowError(ErrorType errorType)
    {
        Console.WriteLine(Messages[errorType]);
    }
}