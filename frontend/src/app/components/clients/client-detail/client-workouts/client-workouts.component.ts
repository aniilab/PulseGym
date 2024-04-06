import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { tap } from 'rxjs';
import { CLIENT } from 'src/app/constants/role-names';
import { WorkoutStatus } from 'src/app/enums/workout-status';
import { WorkoutType } from 'src/app/enums/workout-type';
import { WorkoutViewDTO } from 'src/app/models/workout/workout-view-dto';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-client-workouts',
  templateUrl: './client-workouts.component.html',
  styleUrls: ['./client-workouts.component.css'],
})
export class ClientWorkoutsComponent implements OnInit {
  public workouts: WorkoutViewDTO[];
  public workoutTypes= WorkoutType;
  public workoutStatus=WorkoutStatus;

  constructor(
    private route: ActivatedRoute,
    private workoutService: WorkoutService
  ) {}

  ngOnInit(): void {
    const clientId = this.route.parent.snapshot.paramMap.get('id');

    this.workoutService
      .getUserWorkouts(CLIENT ,clientId)
      .pipe(tap((workouts: WorkoutViewDTO[]) => (this.workouts = workouts)))
      .subscribe();
  }
}
