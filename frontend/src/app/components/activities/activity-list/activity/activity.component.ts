import { Component, Input } from '@angular/core';
import { Activity } from 'src/app/models/activity.model';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css'],
})
export class ActivityComponent {
  @Input()
  public activity: Activity;
}
