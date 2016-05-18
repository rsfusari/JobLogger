using Logger.ConsoleApplication.Logging;
using Logger.ConsoleApplication.Logging.Adapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Logger.UnitTest
{
    public class DataBaseLoggingTest
    {
        [Fact]
        public void CanWriteDataBase()
        {
            var textLogging = new DataBaseLogging();
            var logEntry = new LogEntry();
            logEntry.Message = "Mensaje";
            logEntry.Severity = Severity.Message;

            Assert.True(textLogging.Write(logEntry));
        }

        [Fact]
        public void DataBaseWritingCorrect()
        {
            var textLogging = new FileTextLogging();
            var logEntry = new LogEntry();
            logEntry.Message = "Mensaje";
            logEntry.Severity = Severity.Message;
            textLogging.Write(logEntry);

            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("Insert into Log Values('" + DateTime.Now.ToShortDateString() + " - " + logEntry.Message + "', " + ((int)logEntry.Severity).ToString() + ")", connection);
            command.ExecuteNonQuery();

            SqlCommand commandReader = new SqlCommand("SELECT TOP 1 * FROM Log ORDER BY Id DESC", connection);

            var result = commandReader.ExecuteReader();
            result.Read();
            var dataBaseValue = result.GetInt32(2).ToString() + " - " + result.GetString(1);


            var expectedValue = ((int)logEntry.Severity).ToString() + " - " + DateTime.Now.ToShortDateString() + " - " + logEntry.Message;

            Assert.Equal(expectedValue, dataBaseValue);
        }
    }
}
