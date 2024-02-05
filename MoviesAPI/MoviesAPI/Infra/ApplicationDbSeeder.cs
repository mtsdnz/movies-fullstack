using Microsoft.EntityFrameworkCore;
using MoviesAPI.Domain;

namespace MoviesAPI.Infra;

public class ApplicationDbSeeder
{
    private readonly ILogger<ApplicationDbSeeder> _logger;

    public ApplicationDbSeeder(ILogger<ApplicationDbSeeder> logger)
    {
        _logger = logger;
    }

    public async Task SeedAsync(ApplicationDbContext dbContext)
    {
        await SeedMovies(dbContext);
        await dbContext.SaveChangesAsync();
    }

    private async Task SeedMovies(ApplicationDbContext dbContext)
    {
        // Do not seed the database if we already have movies
        if (await dbContext.Movies.AnyAsync())
            return;

        var dummyMovies = new List<Movie>
        {
            new("The Matrix",
                "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                [
                    new Actor("Keanu Reeves"),
                    new Actor("Laurence Fishburne"),
                    new Actor("Carrie-Anne Moss")
                ]),
            new("Back to the Future",
                "In the 1980s, an experiment by a weird scientist turns out to be faulty. It leads to his teenaged pal going back in time to the 1950s where he is forced to reunite the younger version of his parents.",
                [
                    new Actor("Michael J. Fox"),
                    new Actor("Christopher Lloyd")
                ]),
            new("The Lord of the Rings: The Fellowship of the Ring",
                "A young hobbit, Frodo, who has found the One Ring that belongs to the Dark Lord Sauron, begins his journey with eight companions to Mount Doom, the only place where it can be destroyed.",
                [
                    new Actor("Elijah Wood"),
                    new Actor("Ian McKellen"),
                    new Actor("Orlando Bloom")
                ])
        };

        dummyMovies.ForEach(m =>
        {
            // Add random ratings
            foreach (var i in Enumerable.Range(0, new Random().Next(1, 7)))
            {
                m.Rate((byte)new Random().Next(1, 6));
            }
        });

        dbContext.Movies.AddRange(dummyMovies);
    }
}