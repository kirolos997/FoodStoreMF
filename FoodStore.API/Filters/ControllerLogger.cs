using FoodStore.Core.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace FoodStore.API.Filters
{
    public class ControllerLogger : ActionFilterAttribute
    {
        private readonly ILogger<ControllerLogger> _logger;
        public ControllerLogger(ILogger<ControllerLogger> logger)
        {
            _logger = logger;
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            // our code before action executes
            var controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            var actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;

            _logger.LogInformation("{FilterName}.{MethodName} method", nameof(ControllerLogger), nameof(OnResultExecuted));

            _logger.LogInformation("Calling {ControllerName}.{ActionMethodName} method", controllerName, actionName);


            if (!context.ModelState.IsValid)
            {
                var errorList = context.ModelState.ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray());

                ErrorResponse errorResponse = new ErrorResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    ErrorType = "InvalidModelAttributesError",
                    Message = errorList,


                };
                _logger.LogError(JsonConvert.SerializeObject(new { Error = errorResponse }));

            }


        }

    }
}
