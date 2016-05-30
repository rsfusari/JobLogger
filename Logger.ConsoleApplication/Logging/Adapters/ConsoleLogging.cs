using Logger.ConsoleApplication.Logging.Adapters.Definitions;
using System;

namespace Logger.ConsoleApplication.Logging.Adapters
{
    public class ConsoleLogging : IConsoleLogging
    {
        public ILogEntry GetLogEntry()
        {
            return new LogEntry();
        }

        public bool Write(ILogEntry logEntry)
        {
            if (string.IsNullOrWhiteSpace(logEntry.Message)) return false;
            switch (logEntry.Severity)
            {
                case Severity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Severity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Severity.Message:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine("{0} - {1} - {2}", (int)logEntry.Severity, DateTime.Now.ToShortDateString(), logEntry.Message);

            return true;
        }
    }
}
