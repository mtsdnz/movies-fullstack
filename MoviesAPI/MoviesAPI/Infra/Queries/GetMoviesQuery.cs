using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.ViewModels;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace MoviesAPI.Infra.Queries;

public class GetMoviesQuery : IRequest<List<MovieViewModel>>
{
    /// <summary>
    /// The name of the movie to search for.
    /// </summary>
    public string? Name { get; set; }
}

public class GetMoviesQueryHandler(ApplicationDbContext dbContext, IConfigurationProvider mapperConfiguration)
    : IRequestHandler<GetMoviesQuery, List<MovieViewModel>>
{
    public Task<List<MovieViewModel>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = dbContext.Movies
            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.Name))
            movies = movies.Where(m => EF.Functions.Like(m.Name, $"%{request.Name}%"));

        return movies.ProjectTo<MovieViewModel>(mapperConfiguration)
            .ToListAsync(cancellationToken);
    }
}