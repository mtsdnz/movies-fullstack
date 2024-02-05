import { LoaderFunctionArgs } from 'react-router-dom';
import actorService from '../../data/services/actorService';
import { ActorDetails } from '../../data/models/actor';

export type ActorRouteData = {
  actor: ActorDetails;
};

export async function actorLoader({
  params,
}: LoaderFunctionArgs<{ actorId: string }>): Promise<ActorRouteData> {
  const actor = await actorService.getActor(params.actorId!);
  return { actor };
}
