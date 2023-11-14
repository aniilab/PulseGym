import { Component, Input } from '@angular/core';
import { WorkoutRequest } from 'src/app/models/workout-request.model';

@Component({
  selector: 'app-workout-request',
  templateUrl: './workout-request.component.html',
  styleUrls: ['./workout-request.component.css']
})
export class WorkoutRequestComponent {
  @Input() 
  public workoutRequest: WorkoutRequest;
}
