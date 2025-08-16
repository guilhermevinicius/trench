namespace Trench.User.Domain.Aggregates.Users.Dtos;

public sealed record GetUserByUsernameDto(
    int Id,
    string FirstName,
    string LastName,
    string Username,
    string? Bio,
    bool IsPublic,
    bool IsOwner,
    bool IsFollowPending,
    bool IsFriendShip);