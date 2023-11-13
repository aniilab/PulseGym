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
  ) {
    const client1 = new Client(
      authService.getUser(2),
      'Petro',
      'Petrenko',
      new Date('2000-01-23'),
      'https://static01.nyt.com/newsgraphics/2020/11/12/fake-people/4b806cf591a8a76adfc88d19e90c8c634345bf3d/fallbacks/mobile-03.jpg',
      this.programService.getProgram(0),
      'keep fit',
      70,
      73,
      180,
      this.trainerService.getTrainers[0]
    );

    const client2 = new Client(
      this.authService.getUser(2),
      'Anna',
      'Koval',
      new Date('1996-5-7'),
      'https://kottke.org/plus/misc/images/ai-faces-01.jpg',
      this.programService.getProgram(2),
      'gain weight',
      45,
      46,
      165,
      this.trainerService.getTrainers[0]
    );

    this.clients.push(client1);
    this.clients.push(client2);
  }

  getClients(): Client[] {
    return this.clients.slice();
  }

  getClient(id: number): Client {
    return this.clients.at(id);
  }
}
