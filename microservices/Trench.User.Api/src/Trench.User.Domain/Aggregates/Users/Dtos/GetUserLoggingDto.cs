namespace Trench.User.Domain.Aggregates.Users.Dtos;

public sealed record GetUserLoggingDto(
    string FirstName,
    string LastName,
    string Username);