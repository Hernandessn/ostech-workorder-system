using OSTech.Domain.Exceptions;
using System.Net;

namespace OSTech.WebAPI.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (NotFoundDomainException ex)
        {
            _logger.LogWarning(ex, "Recurso não encontrado.");
            await WriteResponse(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Violação de regra de domínio.");
            await WriteResponse(context, HttpStatusCode.BadRequest, ex.Message);
        }
    }

    private static Task WriteResponse(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsJsonAsync(new { error = message });
    }
}