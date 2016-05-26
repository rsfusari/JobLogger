using Logger.ConsoleApplication.Logging;
using Logger.ConsoleApplication.Logging.Adapters;
using Logger.ConsoleApplication.Logging.Adapters.Definitions;
using Moq;
using NUnit.Framework;
using System;
using System.Configuration;
using System.IO;

namespace Logger.UnitTest
{
    [TestFixture]
    public class FileTextLoggingTest
    {
        private Mock<IFileTextLogging> _logger;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<IFileTextLogging>();
            _logger.Setup(s => s.GetLogEntry()).Returns(new LogEntry { Message = "Message", Severity = Severity.Error });
        }

        [Test]
        public void CanWriteFile()
        {
            var fileTextLogging = new FileTextLogging();

            Assert.True(fileTextLogging.Write(_logger.Object.GetLogEntry()));
        }

        [Test]
        public void FileWritingCorrect()
        {
            var textLogging = new FileTextLogging();
            var logEntry = _logger.Object.GetLogEntry();
            textLogging.Write(logEntry);

            var reader = new StreamReader(ConfigurationManager.AppSettings["LogFileDirectory"]);

            var expectedValue = ((int)logEntry.Severity).ToString() + " - " + DateTime.Now.ToShortDateString() + " - " + logEntry.Message;

            var result = reader.ReadToEnd().Split('\n');
            Assert.That(expectedValue.ToString(), Is.EqualTo(result[result.Length - 2].Replace("\r", string.Empty)));
        }
    }
}
