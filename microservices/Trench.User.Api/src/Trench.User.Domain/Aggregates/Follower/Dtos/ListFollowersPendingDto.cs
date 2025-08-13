namespace Trench.User.Domain.Aggregates.Follower.Dtos;

public sealed record ListFollowersPendingDto(
    int Id,
    string PictureUrl,
    string Name);