namespace Logger.ConsoleApplication.Logging.Definitions
{
    public interface ILogger
    {
        ILogEntry GetLogEntry();

        bool Write(ILogEntry logEntry);
    }
}
