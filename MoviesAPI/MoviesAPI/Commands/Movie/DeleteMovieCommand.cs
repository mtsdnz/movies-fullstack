using MediatR;
using MoviesAPI.Infra;

namespace MoviesAPI.Commands;

public record DeleteMovieCommand(Guid Id) : IRequest<bool>;

public class DeleteMovieCommandHandler(ApplicationDbContext applicationDbContext)
    : IRequestHandler<DeleteMovieCommand, bool>
{
    public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await applicationDbContext.Movies.FindAsync([request.Id],
            cancellationToken: cancellationToken);

        if (movie == null)
            return false;

        applicationDbContext.Remove(movie);
        return await applicationDbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}