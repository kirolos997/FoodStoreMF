using FoodStore.Core.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using System.Text;

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

            // logging querry parameters 
            var httpContext = context.HttpContext;
            var queryParams = httpContext.Request.Query;
            StringBuilder valueString = new StringBuilder();

            foreach (var keyValuePair in queryParams)
            {
                if (keyValuePair.Value.Count > 1)
                {
                    valueString.Append(string.Join(",", keyValuePair.Value));
                }
                else
                {
                    valueString.Append(keyValuePair.Value.FirstOrDefault());
                }

            }
            _logger.LogDebug($"Querry Values: {valueString.ToString()}");

        }

    }
}
