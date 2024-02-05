import { LoaderFunctionArgs } from 'react-router-dom';
import movieService from '../../data/services/movieService';
import { MovieDetails } from '../../data/models/movie';

export type MovieRouteData = {
  movie: MovieDetails;
};

export async function movieLoader({
  params,
}: LoaderFunctionArgs<{ movieId: string }>): Promise<MovieRouteData> {
  const movie = await movieService.getMovie(params.movieId!);
  return { movie };
}
