using Logger.ConsoleApplication.Logging.Adapters.Definitions;
using System;
using System.Data.SqlClient;

namespace Logger.ConsoleApplication.Logging.Adapters
{
    public class DataBaseLogging : IDataBaseLogging
    {
        public ILogEntry GetLogEntry()
        {
            return new LogEntry();
        }

        public bool Write(ILogEntry logEntry)
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
