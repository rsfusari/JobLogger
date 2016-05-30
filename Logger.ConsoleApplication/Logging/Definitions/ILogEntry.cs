namespace Logger.ConsoleApplication.Logging.Definitions
{
    public interface ILogEntry
    {
        string Message { get; set; }

        Severity Severity { get; set; }
    }
}
