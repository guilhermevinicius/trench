namespace Trench.Notification.Domain.Dtos;

public sealed record UserProfileDto(
    int Id,
    string PictureUrl,
    string FirstName,
    string LastName);