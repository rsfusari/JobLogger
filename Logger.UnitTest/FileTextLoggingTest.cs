using Logger.ConsoleApplication.Logging;
using Logger.ConsoleApplication.Logging.Adapters;
using System;
using System.Configuration;
using System.IO;
using Xunit;

namespace Logger.UnitTest
{
    public class FileTextLoggingTest
    {
        [Fact]
        public void CanWriteFile()
        {
            var textLogging = new FileTextLogging();
            var logEntry = new LogEntry();
            logEntry.Message = "Mensaje";
            logEntry.Severity = Severity.Message;

            Assert.True(textLogging.Write(logEntry));
        }

        [Fact]
        public void FileWritingCorrect()
        {
            var textLogging = new FileTextLogging();
            var logEntry = new LogEntry();
            logEntry.Message = "Mensaje";
            logEntry.Severity = Severity.Message;
            textLogging.Write(logEntry);

            var reader = new StreamReader(ConfigurationManager.AppSettings["LogFileDirectory"]);

            var expectedValue = ((int)logEntry.Severity).ToString() + " - " + DateTime.Now.ToShortDateString() + " - " + logEntry.Message;

            var result = reader.ReadToEnd().Split('\n');
            Assert.Equal(expectedValue.ToString(), result[result.Length - 2].Replace("\r", string.Empty));
        }
    }
}
