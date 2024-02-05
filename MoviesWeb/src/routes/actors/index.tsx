import { Link, useLoaderData } from 'react-router-dom';
import { ActorsRouteData } from './actorsLoader';
import ListGroup from 'react-bootstrap/ListGroup';

export function ActorsRoute() {
  const { actors } = useLoaderData() as ActorsRouteData;
  return (
    <>
      <h2>Actors</h2>
      <ListGroup>
        {actors.length === 0 && (
          <ListGroup.Item>Looks like we don't have any actor!</ListGroup.Item>
        )}
        {actors.map((actor) => (
          <ListGroup.Item key={actor.id}>
            <Link to={`/actors/${actor.id}`}>{actor.name}</Link>
          </ListGroup.Item>
        ))}
      </ListGroup>
    </>
  );
}
