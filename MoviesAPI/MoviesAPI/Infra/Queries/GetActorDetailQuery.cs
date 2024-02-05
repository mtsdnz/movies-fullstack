using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.ViewModels;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace MoviesAPI.Infra.Queries;

public class GetActorDetailQuery : IRequest<ActorDetailViewModel>
{
    public Guid Id { get; init; }
}

public class GetActorDetailQueryHandler(ApplicationDbContext dbContext, IConfigurationProvider mapperConfiguration)
    : IRequestHandler<GetActorDetailQuery, ActorDetailViewModel?>
{
    public Task<ActorDetailViewModel?> Handle(GetActorDetailQuery request, CancellationToken cancellationToken)
    {
        return dbContext.Actors
            .Where(m => m.Id == request.Id)
            .ProjectTo<ActorDetailViewModel>(mapperConfiguration)
            .FirstOrDefaultAsync(cancellationToken);
    }
}