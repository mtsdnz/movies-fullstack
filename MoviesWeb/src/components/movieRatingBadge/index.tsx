import Badge from 'react-bootstrap/Badge';
import { FcRating } from 'react-icons/fc';

export function MovieRatingBadge({ value }: { value: number }) {
  return (
    <Badge bg="primary">
      <FcRating /> {value}
    </Badge>
  );
}
