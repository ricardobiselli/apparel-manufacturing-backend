using Domain.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Presentation.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "An unexpected error occurred.";

        if (exception is DomainException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = exception.Message;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        object result;

        if (_env.IsDevelopment())
        {
            // FULL DEBUG INFO
            result = new
            {
                error = exception.Message,
                stackTrace = exception.StackTrace,
                inner = exception.InnerException?.Message
            };
        }
        else
        {
            // SAFE PRODUCTION RESPONSE
            result = new
            {
                error = message
            };
        }

        var json = JsonConvert.SerializeObject(result);
        return context.Response.WriteAsync(json);
    }
}
