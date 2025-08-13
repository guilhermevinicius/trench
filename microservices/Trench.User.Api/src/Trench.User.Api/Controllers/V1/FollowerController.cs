using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trench.User.Api.Configurations.Authentication;
using Trench.User.Application.UseCases.Follower.Commands.Follow;
using Trench.User.Application.UseCases.Follower.Queries.ListFollowerPending;

namespace Trench.User.Api.Controllers.V1;

[Route("api/v1/followers")]
public class FollowerController(
    ISender sender,
    IUserContext userContext)
    : TrenchUserBaseController
{
    /// <summary>
    /// List follower pending
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IResult> ListFollowers(CancellationToken cancellationToken)
    {
        var query = new ListFollowerPendingQuery(userContext.UserIdAsInt());

        var result = await sender.Send(query, cancellationToken);

        return CustomResponse(result, HttpStatusCode.OK);
    }

    /// <summary>
    /// Follow
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IResult> Follow(FollowCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return CustomResponse(result, HttpStatusCode.Created);
    }
}