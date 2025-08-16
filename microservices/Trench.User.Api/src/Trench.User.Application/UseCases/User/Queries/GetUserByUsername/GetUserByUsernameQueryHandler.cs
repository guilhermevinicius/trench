using FluentResults;
using Trench.User.Application.Contracts.Messaging;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.Aggregates.Users.Dtos;
using Trench.User.Domain.Resources;

namespace Trench.User.Application.UseCases.User.Queries.GetUserByUsername;

internal sealed class GetUserByUsernameQueryHandler(
    IUserRepository userRepository) 
    : IQueryHandler<GetUserByUsernameQuery, GetUserByUsernameDto>
{
    public async Task<Result<GetUserByUsernameDto>> Handle(GetUserByUsernameQuery query,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsername(
            query.IdentityId,
            query.Username,
            cancellationToken);
        if (user is null)
            return Result.Fail(DomainValidationResource.UserNotFound);

        return user;
    }
}