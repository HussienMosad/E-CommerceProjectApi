using Domain.Exceptions;
using Shared.ErrorDtos;
using System.Xml;

namespace E_Commerce.Api.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                    await HandleNotFoundApiAsync(context);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Something Went Wrong ==> {ex.Message}");
                await HandleExceptionAsyn(context , ex);
            }
        }

        private async Task HandleNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"The EndPoint With Url {context.Request.Path} Not Found"

            }.ToString();
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsyn(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails
            {

                ErrorMessage = ex.Message
            };
            // 1] Change Status Code
            context.Response.StatusCode = ex switch {
                NotFoundException => StatusCodes.Status404NotFound ,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ValidationException validationException => HandleValidationExcption(validationException , response),
                (_) => StatusCodes.Status500InternalServerError
             };

            response.StatusCode = context.Response.StatusCode;
            await context.Response.WriteAsync(response.ToString());
        }

        private int HandleValidationExcption(ValidationException validationException, ErrorDetails response)
        {
            response.Errors = validationException.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
