import { Link, useLoaderData } from 'react-router-dom';
import { ActorRouteData } from './actorLoader';
import Card from 'react-bootstrap/Card';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
import ListGroup from 'react-bootstrap/ListGroup';
import { MovieRatingBadge } from '../../components/movieRatingBadge';

export function ActorRoute() {
  const { actor } = useLoaderData() as ActorRouteData;
  return (
    <>
      <h1>{actor.name}</h1>
      <Row>
        <Col md={12} xl={8}>
          <Card>
            <Card.Header>Movies</Card.Header>
            <ListGroup variant="flush">
              {actor.movies.map((movie) => (
                <ListGroup.Item key={movie.id}>
                  <Link to={`/movies/${movie.id}`}>{movie.name}</Link>
                  <MovieRatingBadge value={movie.averageRating} />
                </ListGroup.Item>
              ))}
            </ListGroup>
          </Card>
        </Col>
      </Row>
    </>
  );
}
