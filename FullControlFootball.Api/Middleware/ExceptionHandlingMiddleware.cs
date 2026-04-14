using System.Net;
using System.Text.Json;

namespace FullControlFootball.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized request.");
            await WriteProblemDetailsAsync(context, HttpStatusCode.Unauthorized, "Unauthorized", ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Business rule violation.");
            await WriteProblemDetailsAsync(context, HttpStatusCode.BadRequest, "Invalid operation", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled server error.");
            await WriteProblemDetailsAsync(context, HttpStatusCode.InternalServerError, "Server error", "An unexpected error occurred.");
        }
    }

    private static async Task WriteProblemDetailsAsync(HttpContext context, HttpStatusCode statusCode, string title, string detail)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/problem+json";

        var payload = new
        {
            type = "about:blank",
            title,
            status = (int)statusCode,
            detail,
            traceId = context.TraceIdentifier
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
}
