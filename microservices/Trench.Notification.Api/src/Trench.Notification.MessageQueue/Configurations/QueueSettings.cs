namespace Trench.Notification.MessageQueue.Configurations;

public sealed class QueueSettings
{
    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Port { get; set; }
    public required string VirtualHost { get; set; }
}