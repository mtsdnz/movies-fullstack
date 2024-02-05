import { Actor } from './actor';

export type Movie = {
  id: string;
  name: string;
  description: string;
  averageRating: number;
};

export type MovieDetails = Movie & {
  actors: Actor[];
};
