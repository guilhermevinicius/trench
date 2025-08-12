using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trench.User.Application.UseCases.Follower.Commands.Follow;

namespace Trench.User.Api.Controllers.V1;

[Route("api/v1/followers")]
public class FollowerController(
    ISender sender)
    : TrenchUserBaseController
{
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