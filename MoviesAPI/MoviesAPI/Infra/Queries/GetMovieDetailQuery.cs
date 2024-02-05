using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.ViewModels;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace MoviesAPI.Infra.Queries;

public record GetMovieDetailQuery(Guid Id) : IRequest<MovieDetailViewModel?>
{
}

public class GetMovieDetailQueryHandler(ApplicationDbContext dbContext, IConfigurationProvider mapperConfiguration)
    : IRequestHandler<GetMovieDetailQuery, MovieDetailViewModel?>
{
    public Task<MovieDetailViewModel?> Handle(GetMovieDetailQuery request, CancellationToken cancellationToken)
    {
        return dbContext.Movies
            .Where(m => m.Id == request.Id)
            .ProjectTo<MovieDetailViewModel>(mapperConfiguration)
            .FirstOrDefaultAsync(cancellationToken);
    }
}