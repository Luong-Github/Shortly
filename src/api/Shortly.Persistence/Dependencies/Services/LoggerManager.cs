using Serilog;
using Shortly.Contract.Dependencies.Services;

namespace Shortly.Persistence.Dependencies.Services
{
    public class LoggerManager : ILoggerManager
    {
        private readonly ILogger _logger;

        public LoggerManager()
        {
            // Configure Serilog here or use configuration from appsettings.json
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void LogInfo(string message)
        {
            _logger.Information(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warning(message);
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogError(string message, Exception exception)
        {
            _logger?.Error(message, exception);
        }

        public void LogInformation(string message, Exception exception)
        {
            _logger.Information($"{message} {exception}");
        }
    }
}
