import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Activity } from 'src/app/models/activity.model';
import { ActivityViewDTO } from 'src/app/models/activity/activity-view-dto';
import { ActivityService } from 'src/app/services/activity.service';
import { AuthService } from 'src/app/services/auth.service';
import { ConfirmationDialogComponent } from '../../dialogs/confirmation-dialog/confirmation-dialog.component';
import { catchError, of, tap } from 'rxjs';

@Component({
  selector: 'app-activity-list',
  templateUrl: './activity-list.component.html',
  styleUrls: ['./activity-list.component.css'],
})
export class ActivityListComponent implements OnInit {
  public activities: ActivityViewDTO[] = [];
  public currentRole: string = '';

  constructor(
    private activityService: ActivityService,
    private authService: AuthService,
    private matDialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getActivities();

    this.authService.currentRole.subscribe((role: string) => {
      this.currentRole = role;
    });
  }

  getActivities() {
    this.activityService
      .getAllActivities()
      .subscribe(
        (activities: ActivityViewDTO[]) => (this.activities = activities)
      );
  }

  onDelete(id: string) {
    const dialogRef = this.matDialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: 'Are you sure that you want to delete this activity?',
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      if (dialogResult) {
        this.activityService
          .deleteActivity(id)
          .pipe(
            tap(() => this.getActivities()),
            catchError((error) => {
              console.log(error);
              return of(null);
            })
          )
          .subscribe();
      }
    });
  }
}
