using FoodStore.Core.Exceptions.Categories;
using FoodStore.Core.Helpers;
using Newtonsoft.Json;
using System.Net;

namespace FoodStore.API.Middelware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;

            if (exception is InvalidCategoryIDException || exception is ArgumentNullException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }
            else if (exception is DuplicateCategoryException)
            {
                statusCode = (int)HttpStatusCode.Conflict;
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
            }


            // Set the HTTP response status code
            context.Response.StatusCode = statusCode;

            // Write a response message
            context.Response.ContentType = "application/json";

            var responseObject = new ErrorResponse
            {
                StatusCode = statusCode,
                ErrorType = exception.GetType().Name,
                Message = exception.Message
            };

            string jsonString = JsonConvert.SerializeObject(new { Error = responseObject });

            return context.Response.WriteAsync(jsonString);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
