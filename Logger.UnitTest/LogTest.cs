using Logger.ConsoleApplication;
using Logger.ConsoleApplication.Logging;
using Logger.ConsoleApplication.Logging.Adapters;
using Moq;
using NUnit.Framework;
using System;
using System.Text;

namespace Logger.UnitTest
{
    [TestFixture]
    public class LogTest
    {
        [Test]
        public void CanCreateDataBaseInstance()
        {
            var instance = LogService.Log.LoggingOn(ConsoleApplication.Logging.LogOn.DatBase);

            Assert.IsInstanceOf<DataBaseLogging>(instance.Logger);
        }

        [Test]
        public void CanCreateConsoleInstance()
        {
            var instance = LogService.Log.LoggingOn(ConsoleApplication.Logging.LogOn.Console);

            Assert.IsInstanceOf<ConsoleLogging>(instance.Logger);
        }

        [Test]
        public void CanCreateFileTextInstance()
        {
            var instance = LogService.Log.LoggingOn(ConsoleApplication.Logging.LogOn.FileSystem);

            Assert.IsInstanceOf<FileTextLogging>(instance.Logger);
        }

        [Test]
        public void CanSetGlobalInstance()
        {
            LogService.SetLog(new FileTextLogging());

            var instance = LogService.Log.Message("Message").With(ConsoleApplication.Logging.Severity.Warning);

            Assert.IsInstanceOf<FileTextLogging>(instance.Logger);
        }

        [Test]
        public void CanWriteNullMessage()
        {
            LogService.SetLog(new ConsoleLogging());

            var isLogged = LogService.Log.Message(null).With(ConsoleApplication.Logging.Severity.Warning).Write();

            Assert.False(isLogged);
        }

        [Test]
        public void CanWriteNullLoggingType()
        {
            LogService.SetLog(new ConsoleLogging());

            var isLogged = LogService.Log.Message(null).Write();

            Assert.False(isLogged);
        }

        [Test]
        public void GetFullMessageDescription()
        {

            var log = LogService.Log.LoggingOn(ConsoleApplication.Logging.LogOn.FileSystem).Message("Message").With(ConsoleApplication.Logging.Severity.Error);

            var fullDescription = log.GetFullMessage();

            var expectedDescription = new StringBuilder();
            expectedDescription.Append("2 - ").Append(DateTime.Now.ToShortDateString()).Append(" - Message");

            Assert.That(fullDescription, Is.EqualTo(expectedDescription.ToString()));
        }

        [Test]
        public void VerifyILoggerWriteIsCalled()
        {
            //Arrange
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Write(It.IsAny<ILogEntry>())).Verifiable();

            var log = new Log(logger.Object);

            //Act
            log.Write();

            //Assert
            logger.Verify();
        }
    }
}
