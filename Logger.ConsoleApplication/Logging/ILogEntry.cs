namespace Logger.ConsoleApplication.Logging
{
    public interface ILogEntry
    {
        string Message { get; set; }

        Severity Severity { get; set; }
    }
}
