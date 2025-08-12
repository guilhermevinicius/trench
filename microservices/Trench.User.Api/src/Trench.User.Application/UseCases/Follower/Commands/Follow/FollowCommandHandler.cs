using FluentResults;
using Trench.User.Application.Contracts.Messaging;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.Aggregates.Follower.Entities;
using Trench.User.Domain.Resources;
using Trench.User.Domain.SeedWorks;

namespace Trench.User.Application.UseCases.Follower.Commands.Follow;

internal sealed class FollowCommandHandler (
    IUserRepository userRepository,
    IFollowerRepository followerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<FollowCommand, bool>
{
    public async Task<Result<bool>> Handle(FollowCommand command, CancellationToken cancellationToken)
    {
        if (await followerRepository.AlreadyFollowerExists(command.FollowerId, command.FollowingId, cancellationToken))
            return Result.Fail(DomainValidationResource.AlreadyFollowerExists);

        var followingUser = await userRepository.GetById(command.FollowingId, cancellationToken);
        if (followingUser is null)
            return Result.Fail(DomainValidationResource.UserNotFound);

        var follower = Followers.Create(
            command.FollowerId,
            command.FollowingId,
            !followingUser.IsPublic);

        await followerRepository.InsertAsync(follower, cancellationToken);

        return await unitOfWork.CommitAsync(cancellationToken);
    }
}