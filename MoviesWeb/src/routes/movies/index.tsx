import { Link, useLoaderData } from 'react-router-dom';
import { MoviesRouteData } from './moviesLoader';
import { MovieCard } from '../../components/movieCard';
import ListGroup from 'react-bootstrap/ListGroup';

export function MoviesRoute() {
  const { movies } = useLoaderData() as MoviesRouteData;
  return (
    <>
      <h2>Movies</h2>
      <ListGroup variant="flush">
        {movies.length === 0 && (
          <ListGroup.Item>
            Oops! Looks like we don't have any movie!
          </ListGroup.Item>
        )}
        {movies.map((movie) => (
          <ListGroup.Item key={movie.id}>
            <MovieCard
              title={<Link to={`/movies/${movie.id}`}>{movie.name}</Link>}
              description={movie.description}
              rating={movie.averageRating}
            />
          </ListGroup.Item>
        ))}
      </ListGroup>
    </>
  );
}
