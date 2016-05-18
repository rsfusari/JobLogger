using System;

namespace Logger.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            JobLogger.LogMessage("Message1", Logging.Severity.Message);
            JobLogger.LogMessage("Message2", Logging.Severity.Warning);
            JobLogger.LogMessage("Message3", Logging.Severity.Error, true);
            Console.ReadLine();
        }
    }
}
