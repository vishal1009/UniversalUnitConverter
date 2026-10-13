using Microsoft.AspNetCore.Mvc;
using UnitConverter.Api.Conversion;

namespace UnitConverter.Api.Middleware;

public sealed class ApiExceptionMiddleware(
    RequestDelegate next,
    ILogger<ApiExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (UnknownCategoryException ex)
        {
            await WriteProblem(context, StatusCodes.Status400BadRequest,
                "Unsupported category", ex.Message, "category");
        }
        catch (UnknownUnitException ex)
        {
            await WriteProblem(context, StatusCodes.Status400BadRequest,
                "Unsupported unit", ex.Message, "unit");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            await WriteProblem(context, StatusCodes.Status400BadRequest,
                "Value out of range", ex.Message, "value");
        }
        catch (ArgumentException ex)
        {
            await WriteProblem(context, StatusCodes.Status400BadRequest,
                "Invalid conversion request", ex.Message, "request");
        }
        catch (OverflowException ex)
        {
            logger.LogWarning(ex, "Numeric overflow during unit conversion.");
            await WriteProblem(context, StatusCodes.Status400BadRequest,
                "Numeric overflow", "The value is outside the supported numeric range.", "value");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled API exception.");
            await WriteProblem(context, StatusCodes.Status500InternalServerError,
                "Unexpected server error", "An unexpected error occurred.", "server");
        }
    }

    private static async Task WriteProblem(HttpContext context, int status,
        string title, string detail, string code)
    {
        if (context.Response.HasStarted)
            throw new InvalidOperationException("The response has already started.");

        context.Response.Clear();
        context.Response.StatusCode = status;
        context.Response.ContentType = "application/problem+json";

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };
        problem.Extensions["code"] = code;
        await context.Response.WriteAsJsonAsync(problem);
    }
}
