import { Subject } from 'rxjs';
import { Client } from '../models/client.model';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { TrainerService } from './trainer.service';
import { Program } from '../models/program.model';
import { ProgramService } from './program.service';

@Injectable()
export class ClientService {
  public clientsChanged = new Subject<Client[]>();

  private clients: Client[] = [];

  constructor(
    private authService: AuthService,
    private trainerService: TrainerService,
    private programService: ProgramService
  ) {}

  getClients(): Client[] {
    return this.clients.slice();
  }

  getClient(id: number): Client {
    return this.clients.at(id);
  }
}
