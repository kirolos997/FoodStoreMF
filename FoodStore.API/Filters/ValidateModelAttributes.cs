using FoodStore.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FoodStore.API.Filters
{
    public class ValidateModelAttributes : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorList = context.ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());

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

                return; // Stop further filter execution 

            }

        }
    }
}
