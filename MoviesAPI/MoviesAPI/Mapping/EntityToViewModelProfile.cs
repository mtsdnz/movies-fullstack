using AutoMapper;
using MoviesAPI.Domain;
using MoviesAPI.ViewModels;

namespace MoviesAPI.Mapping;

public class EntityToViewModelProfile : Profile
{
    public EntityToViewModelProfile()
    {
        CreateMap<Movie, MovieDetailViewModel>()
            .ForMember(v => v.Actors,
                v => v.MapFrom(f => f.Actors.Select(a => a.Actor)));
        CreateMap<MovieRating, MovieRatingViewModel>();
        CreateMap<Actor, MovieActorViewModel>();
        CreateMap<Movie, MovieViewModel>()
            .ForMember(m => m.AverageRating,
                m => m.MapFrom(f => Math.Round(f.Ratings.Average(r => (int?)r.Rating) ?? 0, 1)));
        CreateMap<Actor, ActorViewModel>();
        CreateMap<Actor, ActorDetailViewModel>()
            .ForMember(a => a.Movies,
                a => a.MapFrom(m => m.Movies.Select(ma => ma.Movie)));
    }
}