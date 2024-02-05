namespace MoviesAPI.Domain;

public class Actor : Entity
{
    public string Name { get; private set; }
    public virtual List<MovieActor>? Movies { get; private set; }

    public Actor(string name) : this()
    {
        SetName(name);
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected Actor()
    {
        Id = Guid.NewGuid();
    }

    public void AddMovie(Movie movie)
    {
        Movies ??= [];
        if (Movies.Any(x => x.MovieId == movie.Id))
            return;

        Movies.Add(new MovieActor { Movie = movie, ActorId = Id });
    }

    public void SetName(string name)
    {
        Name = name;
    }
}