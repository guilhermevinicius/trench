using FluentResults;
using Trench.User.Application.Contracts.Messaging;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.Resources;
using Trench.User.Domain.SeedWorks;

namespace Trench.User.Application.UseCases.User.Commands.Follower;

internal sealed class FollowerCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<FollowerCommand, bool>
{
    public async Task<Result<bool>> Handle(FollowerCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdentityId(command.UserId, cancellationToken);
        if (user is null)
            return Result.Fail(DomainValidationResource.UserNotFound);

        if (user.Id == command.FollowerId)
            return Result.Fail(DomainValidationResource.FollowYourself);

        var userFollower = await userRepository.GetById(command.FollowerId, cancellationToken);
        if (userFollower is null)
            return Result.Fail(DomainValidationResource.UserNotFound);

        if (await userRepository.AlreadyFollowerExists(user.Id, userFollower.Id, cancellationToken))
            return Result.Fail(DomainValidationResource.AlreadyFollowerExists);

        user.AddFollower(command.FollowerId);

        return await unitOfWork.CommitAsync(cancellationToken);
    }
}