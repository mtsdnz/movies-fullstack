using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Application.Exceptions;

namespace MoviesAPI.Middlewares;

/*
* This middleware is responsible for handling exceptions and returning a proper response to the client.
*/
public class ExceptionHandleMiddleware(ILogger<ExceptionHandleMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = new ValidationProblemDetails
        {
            Title = GetTitle(exception),
            Status = statusCode,
            Detail = exception.Message,
            Errors = GetErrors(exception),
            Instance = httpContext.Request.Path
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            RequestValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ValidationException => "Validation Error",
            RequestValidationException => "Validation Error",
            _ => "Server Error"
        };

    private static IDictionary<string, string[]> GetErrors(Exception exception)
    {
        if (exception is RequestValidationException requestValidationException)
            return requestValidationException.Errors;

        if (exception is ValidationException validationException)
            return validationException.Errors
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);


        return null;
    }
}