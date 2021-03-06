﻿namespace Logger.ConsoleApplication.Logging
{
    public interface ILogger
    {
        ILogEntry GetLogEntry();

        bool Write(ILogEntry logEntry);
    }
}
