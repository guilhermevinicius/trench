using FluentResults;
using Trench.User.Application.Contracts.Messaging;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.Aggregates.Users.Dtos;
using Trench.User.Domain.Resources;

namespace Trench.User.Application.UseCases.User.Queries.GetUserLogging;

internal sealed class GetUserLoggingQueryHandler(
    IUserRepository userRepository)
    : IQueryHandler<GetUserLoggingQuery, GetUserLoggingDto>
{
    public async Task<Result<GetUserLoggingDto>> Handle(GetUserLoggingQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserLogging(query.UserId, cancellationToken);
        if (user is null)
            return Result.Fail(DomainValidationResource.UserNotFound);

        return user;
    }
}