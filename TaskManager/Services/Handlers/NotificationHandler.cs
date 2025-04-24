namespace TaskManager.Services.Handlers;

public enum NotificationType
{
    TaskAdded,
    TaskRemoved,
    TaskUpdated,
}

public static class NotificationHandler
{
    private static readonly Dictionary<NotificationType, string> Message = new Dictionary<NotificationType, string>()
    {
        [NotificationType.TaskAdded] = "Новая задача добавлена",
        [NotificationType.TaskRemoved] = "Задача удалена",
        [NotificationType.TaskUpdated] = "Задача изменена"
    };

    public static void ShowMessage(NotificationType notificationType)
    {
        Console.WriteLine(Message[notificationType]);
    }
}