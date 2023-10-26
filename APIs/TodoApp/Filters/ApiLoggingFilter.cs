using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoApp.Filters;
public class ApiLoggingActionFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingActionFilter> Logger;

    public ApiLoggingActionFilter(ILogger<ApiLoggingActionFilter> logger)
    {
        Logger = logger;
    }

    //Executes before the action
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Logger.LogInformation("--------- REQUEST RECEIVED ---------");
        Logger.LogInformation($"TIME: {DateTime.Now.ToLongTimeString()}");
        Logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
        Logger.LogInformation("--------------------- PROCESS START ------------------------");
    }

    //Execute after the action
    public void OnActionExecuted(ActionExecutedContext context)
    {
        Logger.LogInformation("---- RESPONSE ACTION RESULTS ----");
        Logger.LogInformation($"TIME: {DateTime.Now.ToLongTimeString()}");
        Logger.LogInformation($"CONTROLLER: {context.Controller}");
        Logger.LogInformation($"HTTP METHOD: {context.ActionDescriptor.DisplayName}");
        Logger.LogInformation($"RESULT-TYPE: {context.Result}"); ;
        Logger.LogInformation("------------ LOG END -------------");
    }
}