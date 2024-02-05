import actorService from '../../data/services/actorService';
import { Actor } from '../../data/models/actor';

export type ActorsRouteData = {
  actors: Actor[];
};

export async function actorsLoader(): Promise<ActorsRouteData> {
  const actors = await actorService.getActors();
  return { actors };
}
