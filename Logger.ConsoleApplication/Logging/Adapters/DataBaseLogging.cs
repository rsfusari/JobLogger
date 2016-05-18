using System;
using System.Data.SqlClient;

namespace Logger.ConsoleApplication.Logging.Adapters
{
    public class DataBaseLogging : ILogger
    {
        public LogEntry GetLogEntry()
        {
            return new LogEntry();
        }

        public bool Write(LogEntry logEntry)
        {
            try
            {
                SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                connection.Open();

                SqlCommand command = new SqlCommand("Insert into Log Values('" + DateTime.Now.ToShortDateString() + " - " + logEntry.Message + "', " + ((int)logEntry.Severity).ToString() + ")", connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
