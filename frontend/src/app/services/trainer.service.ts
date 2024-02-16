import { Injectable } from '@angular/core';
import { Trainer } from '../models/trainer.model';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { PATH, TRAINER_PATH } from '../constants/uri-paths';
import { Observable, tap } from 'rxjs';
import { TrainerViewDTO } from '../models/trainer/trainer-view-dto';

@Injectable()
export class TrainerService {
  private path: string = PATH + TRAINER_PATH;
  private trainers: Trainer[] = []

  constructor(private http: HttpClient){}

  getTrainers(): Trainer[] {
    return this.trainers.slice();
  }

  getAllTrainers(): Observable<TrainerViewDTO[]> {
    return this.http.get<TrainerViewDTO[]>(this.path).pipe(
      tap((trainers: TrainerViewDTO[])=>{
        trainers.map(trainer=>{
          if (!trainer.imageUrl) {
            trainer.imageUrl = '../../assets/ava.jpg';
          }
        })
      })
    );
  }

  getTrainer(id: number): Trainer {
    return this.trainers[id];
  }
}
