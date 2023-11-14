import { Injectable } from '@angular/core';
import { TrainerService } from './trainer.service';
import { ClientService } from './client.service';
import { WorkoutRequest } from '../models/workout-request.model';

@Injectable()
export class WorkoutRequestService {
  private requests: WorkoutRequest[] = [
    new WorkoutRequest(
      this.trainerService.getTrainer(0),
      this.clientService.getClient(1),
      new Date('2023-11-25'),
      'New'
    ),
    new WorkoutRequest(
        this.trainerService.getTrainer(0),
        this.clientService.getClient(0),
        new Date('2023-11-19'),
        'Accepted'
      ),
  ];

  constructor(private clientService: ClientService, private trainerService: TrainerService){}

  getRequests(): WorkoutRequest[] {
    return this.requests.slice();
  }

  getRequest(id: number): WorkoutRequest {
    return this.requests[id];
  }
}