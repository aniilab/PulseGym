import { Injectable } from '@angular/core';
import { Trainer } from '../models/trainer.model';
import { AuthService } from './auth.service';

@Injectable()
export class TrainerService {
  private trainers: Trainer[] = [
    new Trainer(
      this.authService.getUser(1),
      'Arnold',
      'Schwarzenegger',
      'data:image/jpeg;base64,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',
      'bodybuilding',
      null
    ),
  ];

  constructor(private authService: AuthService){}

  getTrainers(): Trainer[] {
    return this.trainers.slice();
  }

  getTrainer(id: number): Trainer {
    return this.trainers[id];
  }
}