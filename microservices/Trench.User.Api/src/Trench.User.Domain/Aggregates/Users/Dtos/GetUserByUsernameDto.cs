namespace Trench.User.Domain.Aggregates.Users.Dtos;

public sealed record GetUserByUsernameDto(
    int Id,
    string IdentityId,
    string FirstName,
    string LastName,
    string Username,
    string? Bio,
    bool IsPublic,
    bool IsFollowPending,
    bool IsFriendShip)
{
    public bool IsOwner { get; private set; }
    public void SetIsOwner(string identityId) => IsOwner = IdentityId == identityId;
}