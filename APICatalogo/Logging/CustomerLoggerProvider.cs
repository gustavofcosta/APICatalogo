using System.Collections.Concurrent;

namespace APICatalogo.Logging;

public class CustomerLoggerProvider : ILoggerProvider
{
    readonly CustomerLoggerProviderConfig loggerConfig;
    readonly ConcurrentDictionary<string, CustomerLogger> loggers = new ConcurrentDictionary<string, CustomerLogger>();

    public CustomerLoggerProvider(CustomerLoggerProviderConfig config)
    {
        loggerConfig = config;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerConfig));
    }

    public void Dispose()
    {
        loggers.Clear();
    }
}
