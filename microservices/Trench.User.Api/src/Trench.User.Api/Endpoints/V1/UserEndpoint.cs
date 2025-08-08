using System.Net;
using MediatR;
using Trench.User.Api.Extensions;
using Trench.User.Application.UseCases.User.Commands.RegisterUser;

namespace Trench.User.Api.Endpoints.V1;

public class UserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/v1/users")
            .WithTags("Users");

        group.MapPost("register", RegisterUser);
    }

    #region Endpoints

    /// <summary>
    /// Register user
    /// </summary>
    /// <param name="command"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task<IResult> RegisterUser(
        RegisterUserCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return result.CustomResponse(HttpStatusCode.Created);
    }

    #endregion
    
}