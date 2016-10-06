using NLog;

namespace SomeProducts.CrossCutting.ProjectLogger
{
    public static class ProjectLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Trace(string message)
        {
            Logger.Trace(message);
        }

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Warn(string message)
        {
            Logger.Warn(message);
        }

        public static void Error(string message)
        {
            Logger.Error(message);
        }

        public static void Fatal(string message)
        {
            Logger.Fatal(message);
        }

    }
}
