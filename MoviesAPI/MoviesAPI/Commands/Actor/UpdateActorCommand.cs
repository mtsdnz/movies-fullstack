using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Infra;

namespace MoviesAPI.Commands;

public record UpdateActorCommand(Guid Id, string Name, List<Guid>? Movies) : IRequest<bool>;

public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
{
    public UpdateActorCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UpdateActorCommandHandler(ApplicationDbContext applicationDbContext)
    : IRequestHandler<UpdateActorCommand, bool>
{
    public async Task<bool> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
    {
        var actor = await applicationDbContext.Actors.Include(v => v.Movies)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
        
        if (actor == null)
            return false;

        if (request.Movies != null)
        {
            var movies = await applicationDbContext.Movies.Where(x => request.Movies.Contains(x.Id))
                .ToListAsync(cancellationToken);
            movies.ForEach(actor.AddMovie);
        }

        actor.SetName(request.Name);

        applicationDbContext.Update(actor);
        return await applicationDbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}