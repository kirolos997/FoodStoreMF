using FoodStore.Core.Exceptions.Categories;
using System.Net;
using System.Text.Json;

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
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
            }


            // Set the HTTP response status code
            context.Response.StatusCode = statusCode;

            // Write a response message
            context.Response.ContentType = "application/json";

            var responseObject = new
            {
                ExceptionType = exception.GetType().Name,
                ExceptionMessage = exception.Message
            };

            string jsonString = JsonSerializer.Serialize(responseObject);

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
