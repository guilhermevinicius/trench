namespace Trench.Notification.Api.Dtos;

public sealed record CustomResponse(
    bool Success,
    int StatusCode,
    object? Data,
    List<string>? Messages,
    DateTimeOffset DateTimeOffset);