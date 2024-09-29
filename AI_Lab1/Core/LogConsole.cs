using System.Windows.Media;

namespace AI_Lab1.Core;

public static class LogConsole
{
    public static Action<string, SolidColorBrush> LogAction;

    public static void Log(string message)
    {
        LogAction?.Invoke(message, Brushes.White);
    }

    public static void LogSuccess(string message)
    {
        LogAction?.Invoke(message, Brushes.Green);
    }

    public static void LogError(string message)
    {
        LogAction?.Invoke(message, Brushes.Red);
    }

    public static void LogWarning(string message)
    {
        LogAction?.Invoke(message, Brushes.Yellow);
    }
}