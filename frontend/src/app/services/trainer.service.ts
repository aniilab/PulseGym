import { Injectable } from '@angular/core';
import { Trainer } from '../models/trainer.model';
import { AuthService } from './auth.service';

@Injectable()
export class TrainerService {
  private trainers: Trainer[] = []

  constructor(private authService: AuthService){}

  getTrainers(): Trainer[] {
    return this.trainers.slice();
  }

  getTrainer(id: number): Trainer {
    return this.trainers[id];
  }
}
