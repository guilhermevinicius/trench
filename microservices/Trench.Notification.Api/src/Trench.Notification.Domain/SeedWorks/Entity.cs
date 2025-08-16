namespace Trench.Notification.Domain.SeedWorks;

public abstract class Entity 
{
    public int Id { get; protected set; }
    public DateTime CreatedOnUtc { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdateOnUtc { get; protected set; }

    protected Entity()
    {}
}