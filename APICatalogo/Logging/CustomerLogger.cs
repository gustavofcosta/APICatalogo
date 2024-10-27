
namespace APICatalogo.Logging;

public class CustomerLogger : ILogger
{
    readonly string loggerName;

    readonly CustomerLoggerProviderConfig loggerConfig;

    public CustomerLogger(string name, CustomerLoggerProviderConfig config)
    {
        loggerName = name;
        loggerConfig = config;
    }


    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string messagem)
    {
        string caminhoArquivoLog = @"D:\Gustavo_Log.txt";

        using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
        {
            try
            {
                streamWriter.WriteLine(messagem);
                streamWriter.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
   
