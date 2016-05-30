using Logger.ConsoleApplication.Logging;
using Logger.ConsoleApplication.Logging.Adapters.Definitions;
using Moq;
using NUnit.Framework;

namespace Logger.UnitTest
{
    [TestFixture]
    public class ConsoleLoggingTest
    {
        [Test]
        public void VerifyIConsoleLoggingWriteIsCalled()
        {
            //Arrange
            var logger = new Mock<IConsoleLogging>();
            logger.Setup(x => x.Write(It.IsAny<ILogEntry>())).Verifiable();

            var log = new Log(logger.Object);

            //Act
            log.Write();

            //Assert
            logger.Verify();
        }
    }
}
