//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Logger.ConsoleApplication
//{
//    //the solution has some design issues.
//    // - it has the implementation of each way to log in the same method
//    // - every time we want to log we must tell to JobLogger what we want to log and what don't and where we want to log and where don't.
//    // - LogMessage method always write in text plain, in database and in the console. 
//    //   if I want to not logging in database i cann't do it.
//    // - LogMessage logic must be split in each type of logging (database, console, plain text)
//    // - JobLogger must be static. creating instances every time we need it generate overhead.
//    //   if we need to add a new support log, ie emails, 
//    //   we should add a variable to know if we want or not to send an email 
//    //   and make changes across all the code. we must abstract the type of error and where we want to logging.
//    // 
//    public class JobLogger
//    {
//        //Name conventions: variables names are not normalized. we should use this norma to private variables logToFile

//        // "_" is used to declare constants
//        private static bool _logToFile;
//        private static bool _logToConsole;
//        private static bool _logMessage;
//        private static bool _logWarning;
//        private static bool _logError;
//        //"LogToDatabase" is used to declare method, functions, public variables, classes. we sholdn't use it for private variables
//        private static bool LogToDatabase;
//        private bool _initialized;
//        //method with too many parameteres. we should use an object containig the variables as a properties
//        public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase, bool
//    logMessage, bool logWarning, bool logError)
//        {
//            _logError = logError;
//            _logMessage = logMessage;
//            _logWarning = logWarning;
//            LogToDatabase = logToDatabase;
//            _logToFile = logToFile;
//            _logToConsole = logToConsole;
//        }

//        //the method has two parameters with the same name and diferent types.      
//        //parameteres name should correspond with type ie: isMessage for bool, meesage for string  
//        public static void LogMessage(string message, bool message, bool warning, bool error)
//        {
//            //.Trim(): method must be called after check if object is null. If is null you will have a NullReferenceException
//            message.Trim();
//            //this doesn't work beacuse message is ambiguous (is bool or is string)
//            if (message == null || message.Length == 0)
//            {
//                return;
//            }
//            //If we abstract where we want to log, this validation is not needed.
//            if (!_logToConsole && !_logToFile && !LogToDatabase)
//            {
//                throw new Exception("Invalid configuration");
//            }
//            if ((!_logError && !_logMessage && !_logWarning) || (!message && !warning
//    && !error))
//            {
//                throw new Exception("Error or Warning or Message must be specified");
//            }
//            //connection string should be in the connectionString section
//            System.Data.SqlClient.SqlConnection connection = new
//    System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppS
//    ettings["ConnectionString"]);
//            connection.Open();
//            int t;
//            if (message && _logMessage)
//            {
//                t = 1;
//            }
//            if (error && _logError)
//            {
//                t = 2;
//            }
//            if (warning && _logWarning)
//            {
//                t = 3;
//            }
//            //LogToDatabase is never used to know if we log in the database or not. if we alwas log on database, we sholudn't have a variable to the database insert validation.
//            //connection object is not setted
//            System.Data.SqlClient.SqlCommand command = new
//    System.Data.SqlClient.SqlCommand("Insert into Log Values('" + message + "', " +
//    t.ToString() + ")");
//            command.ExecuteNonQuery();

//            string l;
//            if
//    (!System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["Log
//    FileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt")) 
//            {
//                l =
//     //We don't need to read all text into the file, after use it hardest, we will collapse memory.
//     // Is better to user StreamWriter that alows to WriteLines

//     System.IO.File.Open
//    System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["
//    LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"); 
//            }
//            if (error && _logError)
//            {
//                l = l + DateTime.Now.ToShortDateString() + message;
//            }
//            if (warning && _logWarning)
//            {
//                l = l + DateTime.Now.ToShortDateString() + message;
//            }
//            if (message && _logMessage)
//            {
//                l = l + DateTime.Now.ToShortDateString() + message;
//            }

//            //same as before. We don't need to open and write the entire file each time we log.

//            System.IO.File.WriteAllText(System.Configuration.ConfigurationManager.AppSettings[
//            "LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt", l);

//            //based on the design of the solution, at this point, it is possible that any validation will change the color of the console
//            if (error && _logError)
//            {
//                Console.ForegroundColor = ConsoleColor.Red;
//            }
//            if (warning && _logWarning)
//            {
//                Console.ForegroundColor = ConsoleColor.Yellow;
//            }
//            if (message && _logMessage)
//            {
//                Console.ForegroundColor = ConsoleColor.White;
//            }
//            Console.WriteLine(DateTime.Now.ToShortDateString() + message);
//        }
//    }
//}
