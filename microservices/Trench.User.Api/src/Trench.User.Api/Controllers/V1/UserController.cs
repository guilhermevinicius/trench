using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trench.User.Api.Configurations.Authentication;
using Trench.User.Application.UseCases.User.Commands.RegisterUser;
using Trench.User.Application.UseCases.User.Queries.GetUserLogging;

namespace Trench.User.Api.Controllers.V1;

[Route("api/v1/users")]
public class UserController(
    ISender sender,
    IUserContext userContext)
    : TrenchUserBaseController
{
    /// <summary>
    /// Get user logging
    /// </summary>
    /// <returns></returns>
    [HttpGet("me")]
    public async Task<IResult> GetLoggingUser(CancellationToken cancellationToken)
    {
        var query = new GetUserLoggingQuery(
            userContext.UserId());

        var result = await sender.Send(query, cancellationToken);

        return CustomResponse(result, HttpStatusCode.OK);
    }

    /// <summary>
    /// Register user
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IResult> RegisterUser(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return CustomResponse(result, HttpStatusCode.Created);
    }
}