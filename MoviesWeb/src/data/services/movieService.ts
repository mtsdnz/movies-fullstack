import { API_URL } from '../../consts';
import { Movie, MovieDetails } from '../models/movie';

export class MovieService {
  public async getMovies(): Promise<Movie[]> {
    return this.fetch<Movie[]>();
  }

  public async getMovie(id: string): Promise<MovieDetails> {
    return this.fetch<MovieDetails>(id);
  }

  private async fetch<TResponse>(url?: string) {
    const response = await fetch(`${API_URL}/movies/${url ?? ''}`);
    return response.json() as TResponse;
  }
}

const movieService = new MovieService();
export default movieService;
