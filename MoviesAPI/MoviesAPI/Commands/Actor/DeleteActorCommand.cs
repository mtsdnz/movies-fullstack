using MediatR;
using MoviesAPI.Infra;

namespace MoviesAPI.Commands;

public record DeleteActorCommand(Guid Id) : IRequest<bool>;

public class DeleteActorCommandHandler(ApplicationDbContext applicationDbContext)
    : IRequestHandler<DeleteActorCommand, bool>
{
    public async Task<bool> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
    {
        var actor = await applicationDbContext.Actors.FindAsync([request.Id],
            cancellationToken: cancellationToken);

        if (actor == null)
            return false;

        applicationDbContext.Remove(actor);
        return await applicationDbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}