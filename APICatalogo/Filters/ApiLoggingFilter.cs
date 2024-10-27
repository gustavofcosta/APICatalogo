using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiLoggingFilter: IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        //executa antes da Action
        _logger.LogInformation("### Executando => OnActionExecuted");
        _logger.LogInformation("####################################");
        _logger.LogInformation($"{DateTime.Now.ToLongDateString()}");
        _logger.LogInformation($"ModelState : {context.ModelState.IsValid})");
        _logger.LogInformation("####################################");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //executa depois da Action
        _logger.LogInformation("### Executando => OnActionExecuted");
        _logger.LogInformation("####################################");
        _logger.LogInformation($"{DateTime.Now.ToLongDateString()}");
        _logger.LogInformation($"ModelState : {context.HttpContext.Response.StatusCode})");
        _logger.LogInformation("####################################");
    }
}
