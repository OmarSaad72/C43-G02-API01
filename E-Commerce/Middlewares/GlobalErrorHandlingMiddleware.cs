using Domain.Exceptions;
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
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            // set content type => application/json
            httpContext.Response.ContentType = "application/json";
            // C# 8
            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
            // return standard response
            var response = new ErrorDetails
            {
                StatesCode = httpContext.Response.StatusCode,
                ErrorMessage = exception.Message,
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }
    }
}
