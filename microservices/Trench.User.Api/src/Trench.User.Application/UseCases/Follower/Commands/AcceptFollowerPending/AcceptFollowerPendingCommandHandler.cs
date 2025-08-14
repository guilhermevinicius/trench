using FluentResults;
using Trench.User.Application.Contracts.Messaging;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.Resources;
using Trench.User.Domain.SeedWorks;

namespace Trench.User.Application.UseCases.Follower.Commands.AcceptFollowerPending;

internal sealed class AcceptFollowerPendingCommandHandler(
    IFollowerRepository followerRepository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<AcceptFollowerPendingCommand, bool>
{
    public async Task<Result<bool>> Handle(AcceptFollowerPendingCommand command, CancellationToken cancellationToken)
    {
        var followers = await followerRepository.GetFollowerPending(
            command.FollowerId,
            command.FollowingId,
            cancellationToken);

        if (followers is null)
            return Result.Fail(DomainValidationResource.FollowersNotFound);

        if (command.Accepted)
            followers.Accept();
        else
            followers.Reject();

        return await unitOfWork.CommitAsync(cancellationToken);
    }
}