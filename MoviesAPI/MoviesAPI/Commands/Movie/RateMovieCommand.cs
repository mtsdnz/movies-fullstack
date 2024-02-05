using FluentValidation;
using MediatR;
using MoviesAPI.Infra;

namespace MoviesAPI.Commands;

public class RateMovieCommand : IRequest<bool>
{
    /// <summary>
    /// The id of the movie to rate
    /// </summary>
    public Guid MovieId { get; set; }

    /// <summary>
    /// The rate to give to the movie (from 1 to 5)
    /// </summary>
    public byte Rate { get; init; }
}

public class RateMovieCommandValidator : AbstractValidator<RateMovieCommand>
{
    public RateMovieCommandValidator()
    {
        RuleFor(x => x.MovieId).NotEmpty();
        RuleFor(x => x.Rate).InclusiveBetween((byte)1, (byte)5);
    }
}

public class RateMovieCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<RateMovieCommand, bool>
{
    public async Task<bool> Handle(RateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await dbContext.Movies.FindAsync([request.MovieId], cancellationToken);
        if (movie is null)
            return false;

        movie.Rate(request.Rate);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}