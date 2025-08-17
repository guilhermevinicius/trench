using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Trench.Notification.CrossCutting.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase, new()
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;

        try
        {
            logger.LogInformation("Executing request {RequestName}", requestName);

            var result = await next(cancellationToken);

            if (result.IsSuccess)
                logger.LogInformation("Request {RequestName} processed successfully", requestName);
            else
                using (LogContext.PushProperty("Error", result.Errors, true))
                {
                    logger.LogError("Request {RequestName} processed with error", requestName);
                }

            return result;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Request {RequestName} processing failed", requestName);

            throw;
        }
    }
}