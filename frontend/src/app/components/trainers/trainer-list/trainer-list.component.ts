import { Component, OnInit } from '@angular/core';
import { Trainer } from 'src/app/models/trainer.model';
import { TrainerService } from 'src/app/services/trainer.service';
import { AuthService } from 'src/app/services/auth.service';
import { ADMIN } from 'src/app/constants/role-names';
import { TrainerViewDTO } from 'src/app/models/trainer/trainer-view-dto';
import { tap } from 'rxjs';
import { StateService } from 'src/app/services/state.service';

@Component({
  selector: 'app-trainer-list',
  templateUrl: './trainer-list.component.html',
  styleUrls: ['./trainer-list.component.css'],
})
export class TrainerListComponent implements OnInit {
  public currentRole: string = '';
  public adminRole = ADMIN;
  public trainers: TrainerViewDTO[];
  public searchText: string = '';

  constructor(
    private trainerService: TrainerService,
    private authService: AuthService,
    private stateService: StateService
  ) {}

  ngOnInit(): void {
    this.fetchTrainers();

    this.authService.currentRole.subscribe((role: string) => {
      this.currentRole = role;
    });

    this.stateService.trainers$.subscribe(() => {
      this.fetchTrainers();
    })
  }

  private fetchTrainers(): void {
    this.trainerService.getAllTrainers().pipe(
      tap((trainers) => {
        this.trainers = trainers;
      })
    ).subscribe();
  }
}
