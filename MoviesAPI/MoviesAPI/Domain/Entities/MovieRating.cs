namespace MoviesAPI.Domain;

public class MovieRating : Entity
{
    public byte Rating { get; }

    public MovieRating(byte rating)
    {
        Rating = rating;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}