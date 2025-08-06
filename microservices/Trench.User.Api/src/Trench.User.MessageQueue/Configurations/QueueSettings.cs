namespace Trench.User.MessageQueue.Configurations;

public sealed class QueueSettings
{
    public required string Host { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string Port { get; init; }
    public required string VirtualHost { get; init; }
}