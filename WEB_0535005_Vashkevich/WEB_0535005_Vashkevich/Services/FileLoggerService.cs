namespace WEB_0535005_Vashkevich.Services
{
    public class FileLoggerService : ILogger
    {
        private string _filePath;
        private object _lock;

        public FileLoggerService(string filePath) 
        {
            _filePath= filePath;
            _lock= new object();
        }

        public IDisposable BeginScope<TState>(TState state) 
        {
            return null;
        }
        
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Information;
        }
        
        public void Log<TState>(LogLevel logLevel,
            EventId eventId, 
            TState state,
            Exception exception, 
            Func<TState, Exception, string> formatter)
        {
            if (formatter != null) 
            {
                lock (_lock) 
                {
                    File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }

    public class FileLoggerProvider : ILoggerProvider
    {
        private string _filePath;

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLoggerService(_filePath);
        }

        public void Dispose()
        {
            
        }

        public FileLoggerProvider(string filePath)
        {
            _filePath = filePath;
        }
    }
}
