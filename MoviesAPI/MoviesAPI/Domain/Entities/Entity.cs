namespace MoviesAPI.Domain;

public abstract class Entity
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; protected set; }
}