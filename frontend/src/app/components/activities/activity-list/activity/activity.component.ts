import { Component, Input, OnInit, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { ActivityViewDTO } from 'src/app/models/activity/activity-view-dto';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-activity',
  templateUrl: './activity.component.html',
  styleUrls: ['./activity.component.css'],
})
export class ActivityComponent implements OnInit {
  @Input()
  public activity: ActivityViewDTO;
  public currentRole: string;
  @Output('onDelete') activityDelete: Subject<string> = new Subject<string>();

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.currentRole.subscribe((role: string) => {
      this.currentRole = role;
    });
  }

  onDelete() {
    this.activityDelete.next(this.activity.id);
  }
}
