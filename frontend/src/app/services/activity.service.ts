import { Subject} from 'rxjs';
import { Activity } from '../models/activity.model';
import { Injectable } from '@angular/core';

@Injectable()
export class ActivityService {
  public activitiesChanged = new Subject<Activity[]>();

  private activities: Activity[] = [
    new Activity(
      'Football competition',
      'Our gym organized huge football competition!',
      'https://i0.wp.com/sportsclinicfootball.com.au/wp-content/uploads/2023/05/GYM-2.jpg?fit=1920%2C1080&ssl=1',
      new Date('2023-10-25'),
      null
    ),

    new Activity(
      'Stretching in groups',
      'Try stretching in our club!',
      'https://stretch.com/video-platform/static/stretch-community.webp',
      new Date('2023-10-30'),
      null
    ),
  ];

  constructor() {}

  getActivities(): Activity[] {
    return this.activities.slice();
  }

  addActivity(newActivity: Activity) {
    this.activities.push(newActivity);
    this.activitiesChanged.next(this.activities);
  }
}
