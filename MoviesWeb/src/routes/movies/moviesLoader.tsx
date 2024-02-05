import movieService from '../../data/services/movieService';
import { Movie } from '../../data/models/movie';

export type MoviesRouteData = {
  movies: Movie[];
};

export async function moviesLoader(): Promise<MoviesRouteData> {
  const movies = await movieService.getMovies();
  return { movies };
}
