using NLog;

namespace Messanger.Logger
{
    public interface ILogger { 
  
        void Log(LogLevel level, string message);

        void Trace(string message);

        void Error(string message);
    }

    public interface ILogFactory {
        ILogger GetLogger();
    }

    public class Logger : ILogger {

        private readonly NLog.ILogger _logger;

        public Logger(NLog.ILogger logger) {
            _logger = logger;
        }

        public virtual void Log(LogLevel level, string message) {
            _logger.Log(NLog.LogLevel.FromOrdinal((int)level), message);
        }

        public virtual void Trace(string message) {
            Log(LogLevel.TRACE, message);
        }

        public virtual void Error(string message) {
            Log(LogLevel.ERROR, message);
        }
    }

    public class Logger<Class> : Logger {

        public Logger(NLog.ILogger logger) : base(logger) {

        }

        public override void Log(LogLevel level, string message) {
            base.Log(level, $"{typeof(Class)}: {message}");
        }

        public override void Trace(string message) {
            Log(LogLevel.TRACE, message);
        }

        public override void Error(string message) {
            Log(LogLevel.ERROR, message);
        }
    }

    public sealed class LogFactory : ILogFactory {

        private static NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
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
            return new Logger(_logger);
        }

        public ILogger GetLogger<Class>() {
            return new Logger<Class>(_logger);
        }
    }

    public enum LogLevel {
        TRACE, DEBUG, INFO, WARN, ERROR, FATAL, OFF
    }
}