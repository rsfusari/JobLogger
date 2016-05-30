using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.ConsoleApplication.Logging.Definitions
{
    public interface ILog
    {
        ILogger Logger { get; }

        ILog Message(string text);

        ILog LoggingOn(LogOn repository);

        ILog WithMessage(string message);

        ILog With(Severity severity);

        bool Write();

        string GetFullMessage();
    }
}
