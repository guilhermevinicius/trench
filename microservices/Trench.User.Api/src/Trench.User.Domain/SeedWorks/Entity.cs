namespace Trench.User.Domain.SeedWorks;

public abstract class Entity<T> 
{
    public T Id { get; protected set; }
    public DateTime CreatedOnUtc { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdateOnUtc { get; protected set; }
}