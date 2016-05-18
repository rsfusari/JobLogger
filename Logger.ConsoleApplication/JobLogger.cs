using Logger.ConsoleApplication.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Logger.ConsoleApplication
{
    public static class JobLogger
    {
        public static void LogMessage(string message, Severity severity, bool logToDatabase = false)
        {
            List<Severity> logLevels = new List<Severity>();

            foreach (var level in ConfigurationManager.AppSettings["LogLavel"].Split(';'))
            {
                Severity severityLevel;
                if (Enum.TryParse(level, out severityLevel))
                {
                    logLevels.Add(severityLevel);
                }
            }

            if (logLevels.Count > 0)
            {
                if (logLevels.Contains(severity))
                {
                    LogService.Log.LoggingOn(LogOn.FileSystem).Message(message).With(severity).Write();
                    LogService.Log.LoggingOn(LogOn.Console).Message(message).With(severity).Write();

                    if (logToDatabase)
                    {
                        LogService.Log.LoggingOn(LogOn.DatBase).Message(message).With(severity).Write();
                    }
                }
            }            
        }
    }
}
