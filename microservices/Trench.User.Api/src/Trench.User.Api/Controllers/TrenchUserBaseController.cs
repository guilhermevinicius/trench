using System.Net;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trench.User.Api.Dtos;

namespace Trench.User.Api.Controllers;

[Authorize]
[ApiController]
public abstract class TrenchUserBaseController : ControllerBase
{
    public static IResult CustomResponse<T>(Result<T> result, HttpStatusCode httpStatusCode)
    {
        return BuildCustomResponse(result, httpStatusCode);
    }

    #region Private Methods

    private static IResult BuildCustomResponse<TValue>(Result<TValue> response,
        HttpStatusCode httpStatusCode)
    {
        if (response.IsSuccess)
            return httpStatusCode switch
            {
                HttpStatusCode.Created => Results.Created(string.Empty,
                    BuildSuccessResponse(httpStatusCode, response.Value)),
                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.OK => Results.Ok(BuildSuccessResponse(httpStatusCode, response.Value)),
                _ => Results.Ok()
            };

        var (isNotFound, error) = IsNotFound(response);
        return isNotFound switch
        {
            true => BuildNotFoundResponse(error!),
            _ => BuildBadRequestResponse(response)
        };
    }

    private static CustomResponse BuildSuccessResponse<TData>(HttpStatusCode httpStatusCode, TData data)
    {
        return new CustomResponse(true, (int)httpStatusCode, data, null, DateTimeOffset.UtcNow);
    }

    private static IResult BuildBadRequestResponse(IResultBase result)
    {
        var messages = result.Errors.Select(e => e.Message).ToList();
        var resultFail = new CustomResponse(false, (int)HttpStatusCode.BadRequest, null, messages,
            DateTimeOffset.UtcNow);

        return Results.BadRequest(resultFail);
    }

    private static IResult BuildNotFoundResponse(IReason error)
    {
        return Results.NotFound(string.IsNullOrEmpty(error.Message) ? string.Empty : error.Message);
    }

    private static (bool, IError? error) IsNotFound(IResultBase result)
    {
        var error = result.Errors
            .FirstOrDefault(error => error.Metadata.Any(pair => pair.Key == $"{(int)HttpStatusCode.NotFound}"));

        return (error != null, error);
    }

    #endregion
}