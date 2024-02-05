using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.ViewModels;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace MoviesAPI.Infra.Queries;

public class GetActorsQuery : IRequest<List<ActorViewModel>>
{
    /// <summary>
    /// The name of the actor to search for.
    /// </summary>
    public string? Name { get; init; }
}

public class GetActorsQueryHandler(ApplicationDbContext dbContext, IConfigurationProvider mapperConfiguration)
    : IRequestHandler<GetActorsQuery, List<ActorViewModel>>
{
    public Task<List<ActorViewModel>> Handle(GetActorsQuery request, CancellationToken cancellationToken)
    {
        var actors = dbContext.Actors
            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.Name))
            actors = actors.Where(a => EF.Functions.Like(a.Name, $"%{request.Name}%"));

        return actors.ProjectTo<ActorViewModel>(mapperConfiguration)
            .ToListAsync(cancellationToken);
    }
}