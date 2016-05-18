using Logger.ConsoleApplication;
using Logger.ConsoleApplication.Logging.Adapters;
using System;
using System.Linq;
using Xunit;

namespace Logger.UnitTest
{
    public class LogTest
    {
        [Fact]
        public void CanCreateDataBaseInstance()
        {
            var instance = LogService.Log.LoggingOn(ConsoleApplication.Logging.LogOn.DatBase);

            Assert.IsType<DataBaseLogging>(instance.Logger);
        }

        [Fact]
        public void CanCreateConsoleInstance()
        {
            var instance = LogService.Log.LoggingOn(ConsoleApplication.Logging.LogOn.Console);

            Assert.IsType<ConsoleLogging>(instance.Logger);
        }

        [Fact]
        public void CanCreateFileTextInstance()
        {
            var instance = LogService.Log.LoggingOn(ConsoleApplication.Logging.LogOn.FileSystem);

            Assert.IsType<FileTextLogging>(instance.Logger);
        }

        [Fact]
        public void CanSetGlobalInstance()
        {
            LogService.SetLog(new FileTextLogging());

            var instance = LogService.Log.Message("Message").With(ConsoleApplication.Logging.Severity.Warning);

            Assert.IsType<FileTextLogging>(instance.Logger);
        }

        [Fact]
        public void CanWriteNullMessage()
        {
            LogService.SetLog(new ConsoleLogging());

            var isLogged = LogService.Log.Message(null).With(ConsoleApplication.Logging.Severity.Warning).Write();

            Assert.False(isLogged);
        }

        [Fact]
        public void CanWriteNullLoggingType()
        {
            LogService.SetLog(new ConsoleLogging());

            var isLogged = LogService.Log.Message(null).Write();

            Assert.False(isLogged);
        }
       
    }
}
