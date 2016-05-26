namespace Logger.ConsoleApplication.Logging
{
    public class LogEntry: ILogEntry
    {
        public string Message { get; set; }

        public Severity Severity { get; set; }
    }
}
