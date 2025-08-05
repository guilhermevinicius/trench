using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Pulse.Product.Api.Configurations.ExceptionHandler;

public class CustomExceptionHandler(
    ILogger<CustomExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, 
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            // Status = exception switch
            // {
            //     ArgumentException => StatusCodes.Status400BadRequest,
            //     // NotFoundException => StatusCodes.Status404NotFound,
            //     _ => StatusCodes.Status500InternalServerError
            // },
            // Title = exception switch
            // {
            //     ArgumentException => "Invalid Request",
            //     // NotFoundException => "Resource Not Found",
            //     _ => "Internal Server Error"
            // },
            Detail = exception.Message,
            // Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}