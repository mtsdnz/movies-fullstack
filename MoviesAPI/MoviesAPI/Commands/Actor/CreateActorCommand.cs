using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Domain;
using MoviesAPI.Infra;

namespace MoviesAPI.Commands;

public record CreateActorCommand(string Name, List<Guid>? Movies) : IRequest<Guid?>;

public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
{
    public CreateActorCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateActorCommandHandler(ApplicationDbContext applicationDbContext)
    : IRequestHandler<CreateActorCommand, Guid?>
{
    public async Task<Guid?> Handle(CreateActorCommand request, CancellationToken cancellationToken)
    {
        var actor = new Actor(request.Name);
        if (request.Movies != null)
        {
            var movies = await applicationDbContext.Movies.Where(x => request.Movies.Contains(x.Id))
                .ToListAsync(cancellationToken);
            movies.ForEach(actor.AddMovie);
        }

        applicationDbContext.Actors.Add(actor);
        return await applicationDbContext.SaveChangesAsync(cancellationToken) > 0 ? actor.Id : default;
    }
}