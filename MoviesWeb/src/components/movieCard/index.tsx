import Card from 'react-bootstrap/Card';
import { MovieRatingBadge } from '../movieRatingBadge';

export type MovieItemProps = {
  title: string | React.ReactNode;
  description: string;
  rating: number;
};

export function MovieCard({ title, description, rating }: MovieItemProps) {
  return (
    <Card>
      <Card.Body>
        <Card.Title>{title}</Card.Title>
        <Card.Subtitle className="mb-2 text-muted">
          <MovieRatingBadge value={rating} />
        </Card.Subtitle>
        <Card.Text>{description}</Card.Text>
      </Card.Body>
    </Card>
  );
}
