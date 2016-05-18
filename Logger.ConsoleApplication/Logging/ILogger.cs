namespace Logger.ConsoleApplication.Logging
{
    public interface ILogger
    {
        LogEntry GetLogEntry();

        bool Write(LogEntry logEntry);
    }
}
