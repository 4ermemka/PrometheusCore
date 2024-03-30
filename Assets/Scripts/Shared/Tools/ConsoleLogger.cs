using System;

namespace Assets.Scripts.Shared.Tools
{
    public static class ConsoleLogger
    {
        public static Action<string, string> OnLogInformation { get; set; }
        public static Action<string, string> OnLogWarning { get; set; }
        public static Action<string, string> OnLogError { get; set; }
        public static Action<string, Exception> OnLogException { get; set; }

        public static void LogInformation(string source, string message)
        {
            OnLogInformation?.Invoke(source, message);
        }
        public static void LogWarning(string source, string message)
        {
            OnLogWarning?.Invoke(source, message);

        }
        public static void LogError(string source, string message)
        {
            OnLogError?.Invoke(source, message);
        }

        public static void LogException(string source, Exception exception)
        {
            OnLogException?.Invoke(source, exception);
        }
    }
}
