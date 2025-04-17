using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net;
using System.Reflection.Metadata;

namespace E_Commerce.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    await HandleNotFoundAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong{ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleNotFoundAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatesCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"The End Point{httpContext.Request.Path}"
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }

        public async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            // set default status code 500
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //500
            // set content type => application/json
            httpContext.Response.ContentType = "application/json";
            // C# 8
            var response = new ErrorDetails
            {
                ErrorMessage = exception.Message,
            };

            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound, // 404
                UnAuthorizedException => (int)HttpStatusCode.Unauthorized, //401
                ValidationException validationException => handleValidation(validationException, response),
                _ => (int)HttpStatusCode.InternalServerError  //500
            };
            // return standard response
            response.StatesCode = httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsync(response.ToString());
        }

        private int handleValidation(ValidationException validationException, ErrorDetails response)
        {
            response.Errors = validationException.Errors;
            return (int)HttpStatusCode.BadRequest;
        }
    }
}
