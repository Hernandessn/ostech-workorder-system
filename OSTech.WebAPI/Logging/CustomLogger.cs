namespace OSTech.WebAPI.Logging
{
    public class CustomerLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            string message = $"{logLevel}: {loggerName} [{eventId.Id}] - {formatter(state, exception)}";

            if (exception != null)
                message += $"\n{exception}";

            WriteTerminal(message);
        }
        public void WriteTerminal(string message)
        {
            Console.WriteLine(message);
        }
    }
}
