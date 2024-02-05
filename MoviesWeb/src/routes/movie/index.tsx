import { Link, useLoaderData } from 'react-router-dom';
import { MovieRouteData } from './movieLoader';
import Card from 'react-bootstrap/Card';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
import ListGroup from 'react-bootstrap/ListGroup';
import { MovieRatingBadge } from '../../components/movieRatingBadge';

export function MovieRoute() {
  const { movie } = useLoaderData() as MovieRouteData;
  return (
    <>
      <h1>{movie.name}</h1>
      <Row>
        <Col md={12} xl={8}>
          <MovieRatingBadge value={movie.averageRating} />
          <p className="mb-2 text-muted">{movie.description}</p>

          <Card>
            <Card.Header>Actors</Card.Header>
            <ListGroup variant="flush">
              {movie.actors.length === 0 && (
                <ListGroup.Item>
                  Looks like we don't have any actor for this movie!
                </ListGroup.Item>
              )}
              {movie.actors.map((actor) => (
                <ListGroup.Item key={actor.id}>
                  <Link to={`/actors/${actor.id}`}>{actor.name}</Link>
                </ListGroup.Item>
              ))}
            </ListGroup>
          </Card>
        </Col>
      </Row>
    </>
  );
}
