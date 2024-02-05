using MoviesAPI.Domain.Exception;

namespace MoviesAPI.Domain;

public class Movie : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public List<MovieActor> Actors { get; set; }
    public List<MovieRating>? Ratings { get; set; }

    public Movie(string name, string description, List<Actor>? actors = null) : this()
    {
        SetName(name);
        SetDescription(description);
        actors?.ForEach(AddActor);
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected Movie()
    {
        Id = Guid.NewGuid();
    }

    public void Rate(byte rating)
    {
        if (rating is < 1 or > 5)
            throw new DomainException("Rating must be between 1 and 5");

        Ratings ??= [];
        Ratings.Add(new MovieRating(rating));
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetDescription(string description)
    {
        Description = description;
    }

    public void AddActor(Actor actor)
    {
        Actors ??= [];

        if (Actors.Any(x => x.ActorId == actor.Id))
            return;

        Actors.Add(new MovieActor { Actor = actor, MovieId = Id });
    }
}

public class MovieActor
{
    public Guid MovieId { get; set; }
    public virtual Movie Movie { get; set; }
    public Guid ActorId { get; set; }
    public virtual Actor Actor { get; set; }
}