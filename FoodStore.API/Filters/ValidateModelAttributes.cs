using FoodStore.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace FoodStore.API.Filters
{
    public class ValidateModelAttributes : ActionFilterAttribute
    {
        private readonly ILogger<ValidateModelAttributes> _logger;
        public ValidateModelAttributes(ILogger<ValidateModelAttributes> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorList = context.ModelState.ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray());

                ErrorResponse errorResponse = new ErrorResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    ErrorType = "InvalidModelAttributesError",
                    Message = errorList,


                };
                context.Result = new ObjectResult(new { Error = errorResponse })
                {
                    StatusCode = errorResponse.StatusCode // Ensure matching status code
                };

                _logger.LogError(JsonConvert.SerializeObject(context.Result));

                return; // Stop further filter execution 

            }

        }
    }
}
