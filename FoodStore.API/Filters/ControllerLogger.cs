using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace FoodStore.API.Filters
{
    public class ControllerLogger : IActionFilter
    {
        private readonly ILogger<ControllerLogger> _logger;
        public ControllerLogger(ILogger<ControllerLogger> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // our code before action executes
            var controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            var actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;

            _logger.LogInformation("{FilterName}.{MethodName} method", nameof(ControllerLogger), nameof(OnActionExecuting));

            _logger.LogInformation("{ControllerName}.{ActionMethodName} method", controllerName, actionName);

            // Get the ActionArguments dictionary
            var arguments = context.ActionArguments;

            // Loop through each argument
            foreach (var (key, value) in arguments)
            {
                if (value is object)
                {
                    _logger.LogDebug("Argument Name: {key}, Argument Value: {value}", key, JsonConvert.SerializeObject(value));
                }
                else
                {
                    _logger.LogDebug("Argument Name: {key}, Argument Value: {value}", key, value);
                }

            }

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
            _logger.LogInformation("{FilterName}.{MethodName} method", nameof(ControllerLogger), nameof(OnActionExecuting));

        }
    }
}
