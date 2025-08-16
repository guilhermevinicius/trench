namespace Trench.User.Domain.Aggregates.Follower.Dtos;

public sealed record FollowersDto(
    int FollowerId,
    int FollowingId,
    bool IsRequired,
    bool? Accepted);