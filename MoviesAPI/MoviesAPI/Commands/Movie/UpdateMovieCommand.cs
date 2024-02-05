using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Infra;

namespace MoviesAPI.Commands;

public class UpdateMovieCommand : IRequest<bool>
{
    /// <summary>
    /// Id of the movie to update
    /// </summary>
    public Guid Id { get; init; }

    public string Name { get; init; }
    public string Description { get; init; }

    /// <summary>
    /// Ids of the actors to add to the movie
    /// </summary>
    public List<Guid>? Actors { get; init; }
}

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}

public class UpdateMovieCommandHandler(ApplicationDbContext applicationDbContext)
    : IRequestHandler<UpdateMovieCommand, bool>
{
    public async Task<bool> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await applicationDbContext.Movies.Include(v => v.Actors)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (movie == null)
            return false;

        if (request.Actors != null) // Only update actors if the request has actors
        {
            var actors = await applicationDbContext.Actors.Where(x => request.Actors!.Contains(x.Id))
                .ToListAsync(cancellationToken);
            actors.ForEach(movie.AddActor);
        }

        movie.SetName(request.Name);
        movie.SetDescription(request.Description);

        applicationDbContext.Update(movie);
        return await applicationDbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}