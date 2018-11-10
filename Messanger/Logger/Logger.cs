namespace Messanger.Logger
{
    public interface ILogger { 
  
        void Log(NLog.LogLevel level, string message);
    }

    public interface ILogFactory {

        ILogger GetLogger();
    }

    public sealed class Logger : ILogger {

        private static NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void Log(NLog.LogLevel level, string message) {
            _logger.Log(level, message);
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
}