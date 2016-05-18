using Logger.ConsoleApplication.Infraestructure;
using Logger.ConsoleApplication.Logging.Adapters;

namespace Logger.ConsoleApplication.Logging
{
    public class Log
    {
        #region Attributes

        private ILogger logger;
        private LogEntry entry;

        #endregion

        #region Public Properties
        public ILogger Logger
        {
            get
            {
                return logger;
            }
        }
        #endregion

        #region Constructor

        public Log() { }

        public Log(ILogger logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Public functions
        public Log Message(string text)
        {
            return this.Build().WithMessage(text);
        }

        public Log LoggingOn(LogOn repository)
        {
            this.SetRepositoryInstance(repository);
            return this.Build();
        }

        public Log WithMessage(string message)
        {
            this.entry.Message = message != null ? message.Trim() : string.Empty;
            return this;
        }

        public Log With(Severity severity)
        {
            this.entry.Severity = severity;
            return this;
        }

        public bool Write()
        {
            return this.logger.Write(this.entry);
        }
        #endregion

        #region Private
        private void SetRepositoryInstance(LogOn repository)
        {
            switch (repository)
            {
                case LogOn.FileSystem:
                    this.logger = Factory.GetInstance<FileTextLogging>();
                    break;
                case LogOn.Console:
                    this.logger = Factory.GetInstance<ConsoleLogging>();
                    break;
                case LogOn.DatBase:
                    this.logger = Factory.GetInstance<DataBaseLogging>();
                    break;
                default:
                    break;
            }

        }

        private Log Build()
        {
            if (this.entry == null)
            {
                this.entry = this.logger.GetLogEntry();
            }
            return this;
        }
        #endregion
    }
}
