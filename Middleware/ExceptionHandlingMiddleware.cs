using System.Net;
using System.Text.Json;
using HorsePedigree_2026.DTOs.Common;
using HorsePedigree_2026.Exceptions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace HorsePedigree_2026.Middleware;

public class ExceptionHandlingMiddleware
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message, detail) = MapException(exception);

        if (statusCode >= (int)HttpStatusCode.InternalServerError)
        {
            _logger.LogError(exception, "Error no controlado: {Message}", exception.Message);
        }
        else
        {
            _logger.LogWarning(exception, "Error de cliente: {Message}", exception.Message);
        }

        var response = new ApiErrorResponse
        {
            StatusCode = statusCode,
            Message = message,
            Detail = detail,
            TraceId = context.TraceIdentifier
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonOptions));
    }

    private static (int StatusCode, string Message, string? Detail) MapException(Exception exception)
    {
        if (exception is DbUpdateException dbUpdate)
        {
            return MapDbUpdateException(dbUpdate);
        }

        return exception switch
        {
            NotFoundException notFound => (
                (int)HttpStatusCode.NotFound,
                notFound.Message,
                null),
            BusinessException business => (
                (int)HttpStatusCode.BadRequest,
                business.Message,
                null),
            UnauthorizedException unauthorized => (
                (int)HttpStatusCode.Unauthorized,
                unauthorized.Message,
                null),
            ArgumentException argument => (
                (int)HttpStatusCode.BadRequest,
                "Solicitud inválida.",
                argument.Message),
            UnauthorizedAccessException => (
                (int)HttpStatusCode.Unauthorized,
                "No autorizado.",
                null),
            _ => (
                (int)HttpStatusCode.InternalServerError,
                "Ocurrió un error interno en el servidor.",
                exception.Message)
        };
    }

    private static (int StatusCode, string Message, string? Detail) MapDbUpdateException(DbUpdateException exception)
    {
        var postgres = FindPostgresException(exception);
        if (postgres is null)
        {
            return (
                (int)HttpStatusCode.InternalServerError,
                "Ocurrió un error interno en el servidor.",
                exception.InnerException?.Message ?? exception.Message);
        }

        return postgres.SqlState switch
        {
            PostgresErrorCodes.UniqueViolation => (
                (int)HttpStatusCode.BadRequest,
                "Ya existe un registro con esos datos.",
                postgres.Detail ?? postgres.MessageText),
            PostgresErrorCodes.ForeignKeyViolation => (
                (int)HttpStatusCode.BadRequest,
                "Una referencia no es válida o no existe.",
                postgres.MessageText),
            PostgresErrorCodes.CheckViolation => (
                (int)HttpStatusCode.BadRequest,
                "Los datos no cumplen las reglas de la base de datos.",
                postgres.MessageText),
            _ => (
                (int)HttpStatusCode.InternalServerError,
                "Ocurrió un error al guardar en la base de datos.",
                postgres.MessageText)
        };
    }

    private static PostgresException? FindPostgresException(Exception exception)
    {
        for (var current = exception; current is not null; current = current.InnerException)
        {
            if (current is PostgresException postgres)
            {
                return postgres;
            }
        }

        return null;
    }
}
