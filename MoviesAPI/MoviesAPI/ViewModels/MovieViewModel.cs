namespace MoviesAPI.ViewModels;

public class MovieViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public double AverageRating { get; init; }
}

public class MovieDetailViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public List<MovieActorViewModel>? Actors { get; init; }
    public List<MovieRatingViewModel>? Ratings { get; init; }
    public double AverageRating => Math.Round(Ratings?.Average(r => (int?)r.Rating) ?? 0, 1);
}

public class MovieActorViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}

public class MovieRatingViewModel
{
    public Guid Id { get; init; }
    public byte Rating { get; init; }
}