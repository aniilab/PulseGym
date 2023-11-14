import { Component } from '@angular/core';
import { WorkoutRequest } from 'src/app/models/workout-request.model';
import { AuthService } from 'src/app/services/auth.service';
import { WorkoutRequestService } from 'src/app/services/workout-request.service';

@Component({
  selector: 'app-workout-request-list',
  templateUrl: './workout-request-list.component.html',
  styleUrls: ['./workout-request-list.component.css']
})
export class WorkoutRequestListComponent {
  public requests: WorkoutRequest[];
  public currentRole: string = '';

  constructor(
    private requestService: WorkoutRequestService
  ) {}

  ngOnInit(): void {
    this.requests = this.requestService.getRequests();
  }
}
