using Microsoft.AspNetCore.Mvc;
using MiniJiraApi.Application.Common;

namespace MiniJiraApi.API.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next,  ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ConflictException exception)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;

            _logger.LogWarning(exception, "Conflict while processing {Method} {Path}", context.Request.Method,
                context.Request.Path);

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflict",
                Detail = "A conflict occurred while processing the request."
            };

            await context.Response.WriteAsJsonAsync(problemDetails, options: null,
                contentType: "application/problem+json");
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
            _logger.LogError(exception, "Unhandled exception while processing {Method} {Path}", context.Request.Method,
                context.Request.Path);

            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred while processing the request."
            };
            
            await context.Response.WriteAsJsonAsync(problemDetails, options: null,
                contentType: "application/problem+json");
        }
    }
}