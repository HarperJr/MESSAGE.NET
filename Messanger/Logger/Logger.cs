namespace Messanger.Logger
{
    public interface ILogger { 
  
        void Log(LogLevel level, string message);
    }

    public interface ILogFactory {

        ILogger GetLogger();
    }

    public sealed class Logger : ILogger {

        private static NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void Log(LogLevel level, string message) {
            _logger.Log(NLog.LogLevel.FromOrdinal((int)level), message);
        }
    }

    public sealed class LogFactory : ILogFactory {

        private static LogFactory _instance;

        public static LogFactory Factory {
            get {
                if (_instance == null) {
                    _instance = new LogFactory();
                }
                return _instance;
            }
        }

        public ILogger GetLogger() {
            return new Logger();
        }
    }

    public enum LogLevel {
        TRACE, DEBUG, INFO, WARN, ERROR, FATAL, OFF
    }
}