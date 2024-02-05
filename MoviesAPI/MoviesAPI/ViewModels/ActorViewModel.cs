namespace MoviesAPI.ViewModels;

public class ActorViewModel
{
    /// <summary>
    /// Actor's unique identifier.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Actor's name.
    /// </summary>
    public string Name { get; init; }
}

public class ActorDetailViewModel
{
    /// <summary>
    /// Actor's unique identifier.
    /// </summary>
    public Guid Id { get; init; }
    /// <summary>
    /// Actor's name.
    /// </summary>
    public string Name { get; init; }
    /// <summary>
    /// Movies in which the actor has participated.
    /// </summary>
    public List<MovieViewModel>? Movies { get; init; }
}