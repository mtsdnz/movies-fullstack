import { API_URL } from '../../consts';
import { Actor, ActorDetails } from '../models/actor';

export class ActorService {
  public async getActors(): Promise<Actor[]> {
    return this.fetch<Actor[]>();
  }

  public async getActor(id: string): Promise<ActorDetails> {
    return this.fetch<ActorDetails>(id);
  }

  private async fetch<TResponse>(url?: string) {
    const response = await fetch(`${API_URL}/actors/${url ?? ''}`);
    return response.json() as TResponse;
  }
}

const actorService = new ActorService();
export default actorService;
