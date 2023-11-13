import { Component, OnInit, Input } from '@angular/core';
import { Activity } from 'src/app/models/activity.model';
import { ActivityService } from 'src/app/services/activity.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-activity-list',
  templateUrl: './activity-list.component.html',
  styleUrls: ['./activity-list.component.css'],
})
export class ActivityListComponent implements OnInit {
  public activities: Activity[] = [];
  public currentRole: string = '';

  constructor(
    private activityService: ActivityService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.getActivitiesList();
    this.activityService.activitiesChanged.subscribe((activities) => {
      this.activities = activities;
    });

    this.currentRole = this.authService.getRole();
    this.authService.authRoleChanged.subscribe((role: string) => {
      this.currentRole = role;
    });
  }

  getActivitiesList() {
    this.activities = this.activityService.getActivities();
  }
}
