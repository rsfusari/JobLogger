using Logger.ConsoleApplication.Logging;

namespace Logger.ConsoleApplication
{
    public static class LogService
    {
        private static Log log;

        public static Log Log
        {
            get
            {
                if (log != null)
                {
                    return log;
                }
                else
                {
                    return new Log();
                }
            }
        }

        public static void SetLog(ILogger logger)
        {
            LogService.log = new Log(logger);
        }
    }
}
