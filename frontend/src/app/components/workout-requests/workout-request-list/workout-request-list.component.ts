import { Component } from '@angular/core';
import { WorkoutRequest } from 'src/app/models/workout-request.model';

@Component({
  selector: 'app-workout-request-list',
  templateUrl: './workout-request-list.component.html',
  styleUrls: ['./workout-request-list.component.css']
})
export class WorkoutRequestListComponent {
  public requests: WorkoutRequest[];
  public currentRole: string = '';

  constructor(
    // private requestService: WorkoutRequestService
  ) {}

  ngOnInit(): void {
    // this.requests = this.requestService.getRequests();
  }
}
