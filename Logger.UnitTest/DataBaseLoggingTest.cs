using Logger.ConsoleApplication.Logging;
using Logger.ConsoleApplication.Logging.Adapters;
using Logger.ConsoleApplication.Logging.Adapters.Definitions;
using Moq;
using NUnit.Framework;
using System;
using System.Data.SqlClient;


namespace Logger.UnitTest
{
    [TestFixture]
    public class DataBaseLoggingTest
    {
        private Mock<IDataBaseLogging> _logger;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<IDataBaseLogging>();
            _logger.Setup(s => s.GetLogEntry()).Returns(new LogEntry { Message = "Message", Severity = Severity.Error });
        }

        [Test]
        public void CanWriteDataBase()
        {
            var dataBaseLogging = new DataBaseLogging();
            Assert.True(dataBaseLogging.Write(_logger.Object.GetLogEntry()));
        }

        [Test]
        public void DataBaseWritingCorrect()
        {
            var dataBaseLogging = new DataBaseLogging();
            var logEntry = _logger.Object.GetLogEntry();

            dataBaseLogging.Write(logEntry);

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            connection.Open();

            SqlCommand commandReader = new SqlCommand("SELECT TOP 1 * FROM Log ORDER BY Id DESC", connection);

            var result = commandReader.ExecuteReader();
            result.Read();
            var dataBaseValue = result.GetInt32(2).ToString() + " - " + result.GetString(1);


            var expectedValue = ((int)logEntry.Severity).ToString() + " - " + DateTime.Now.ToShortDateString() + " - " + logEntry.Message;

            Assert.That(expectedValue, Is.EqualTo(dataBaseValue));
        }
    }
}
