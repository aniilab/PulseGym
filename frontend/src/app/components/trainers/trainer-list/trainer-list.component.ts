import { Component, OnInit } from '@angular/core';
import { Trainer } from 'src/app/models/trainer.model';
import { TrainerService } from 'src/app/services/trainer.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-trainer-list',
  templateUrl: './trainer-list.component.html',
  styleUrls: ['./trainer-list.component.css'],
})
export class TrainerListComponent implements OnInit {
  public currentRole: string = '';
  public trainers: Trainer[];

  constructor(
    private trainerService: TrainerService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.trainers = this.trainerService.getTrainers();

    // this.currentRole = this.authService.getRole();
    // this.authService.authRoleChanged.subscribe((role: string) => {
    //   this.currentRole = role;
    // });
  }
}
