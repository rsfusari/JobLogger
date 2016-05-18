﻿using System;
using System.Configuration;
using System.IO;
namespace Logger.ConsoleApplication.Logging.Adapters
{
    public class FileTextLogging : ILogger
    {
        public LogEntry GetLogEntry()
        {
            return new LogEntry();
        }

        public bool Write(LogEntry logEntry)
        {
            try
            {
                var stream = new StreamWriter(ConfigurationManager.AppSettings["LogFileDirectory"], true);

                stream.WriteLine("{0} - {1} - {2}", (int)logEntry.Severity, DateTime.Now.ToShortDateString(), logEntry.Message);

                stream.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
