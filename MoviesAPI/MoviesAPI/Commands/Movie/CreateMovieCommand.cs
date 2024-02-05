using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Domain;
using MoviesAPI.Infra;

namespace MoviesAPI.Commands;

/// <summary>
/// Command to create a movie
/// </summary>
public class CreateMovieCommand : IRequest<Guid?>
{
    /// <summary>
    /// The name of the movie
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// The description of the movie
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// The ids of the actors that are in the movie
    /// </summary>
    public List<Guid>? Actors { get; init; }
}

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        // TODO: Validate if actors exist
    }
}

public class CreateMovieCommandHandler(ApplicationDbContext applicationDbContext)
    : IRequestHandler<CreateMovieCommand, Guid?>
{
    public async Task<Guid?> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        // TODO: Should we check if there is already a movie with the same name?
        var actors = await applicationDbContext.Actors.Where(x => request.Actors!.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var movie = new Movie(request.Name, request.Description);
        actors.ForEach(movie.AddActor);

        applicationDbContext.Add(movie);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return movie.Id;
    }
}