import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { tap, catchError, take, map } from 'rxjs/operators';
import { CanComponentDeactivate } from 'src/app/guards/deactivate-guard.service';
import { ActivityInDTO } from 'src/app/models/activity/activity-in-dto';
import { ActivityViewDTO } from 'src/app/models/activity/activity-view-dto';
import { ActivityService } from 'src/app/services/activity.service';
import { ConfirmationDialogComponent } from '../../dialogs/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-activity-form',
  templateUrl: './activity-form.component.html',
  styleUrls: ['./activity-form.component.css'],
})
export class ActivityFormComponent implements OnInit, CanComponentDeactivate {
  public activityForm: FormGroup;
  public minDate = Date.now();

  private activityEditId: string;
  private changesSaved: boolean = false;
  private createMode: boolean;
  private newActivity: ActivityInDTO;
  private oldActivity: ActivityViewDTO;

  constructor(
    private activityService: ActivityService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private matDialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.determineEditMode();

    if (!this.createMode) {
      this.fillFormWithActivity();
    }
  }

  initializeForm(): void {
    this.activityForm = this.formBuilder.group({
      title: [null, Validators.required],
      description: [null, Validators.required],
      imageUrl: [null],
      date: [new Date()],
    });
  }

  determineEditMode(): void {
    const routeSegment = this.router.url.split('/')[2];
    this.createMode = routeSegment === 'create';
    if (!this.createMode) {
      this.activityEditId = this.route.snapshot.paramMap.get('id');
    }
  }

  fillFormWithActivity(): void {
    if (!this.activityEditId) {
      throw new Error('No activity id was passed');
    }

    this.activityService
      .getActivity(this.activityEditId)
      .pipe(
        tap((activity) => {
          this.oldActivity = activity;
          this.activityForm.patchValue({
            title: activity.title,
            description: activity.description,
            imageUrl: activity.imageUrl,
            date: activity.dateTime,
          });
        }),
        catchError((error) => {
          console.error('Error: ', error);
          return of(null);
        })
      )
      .subscribe();
  }

  onSubmit(): void {
    this.createActivity();
    this.createMode ? this.addActivity() : this.editActivity();
  }

  createActivity() {
    const _title = this.activityForm.get('title').value;
    const _description = this.activityForm.get('description').value;
    const _imageUrl = this.activityForm.get('imageUrl').value;
    const _date = new Date(this.activityForm.get('date').value); 
    const _dateString = new Date(_date.getTime() - (_date.getTimezoneOffset() * 60000)).toISOString();

    this.newActivity = new ActivityInDTO(
      _title,
      _description,
      _dateString,
      _imageUrl
    );
  }

  addActivity(): void {
    this.activityService
      .addActivity(this.newActivity)
      .pipe(
        tap(() => {
          this.resetFormAndNavigate('..');
        })
      )
      .subscribe();
  }

  editActivity(): void {
    if (this.newActivity && this.isFormChanged()) {
      this.activityService
        .updateActivity(this.activityEditId, this.newActivity)
        .pipe(
          tap(() => {
            this.resetFormAndNavigate('../..');
          })
        )
        .subscribe();
    } else if (!this.isFormChanged()) {
      this.resetFormAndNavigate('../..');
    }
  }

  resetFormAndNavigate(relativePath: string): void {
    this.activityForm.reset();
    this.changesSaved = true;
    this.router.navigate([relativePath], { relativeTo: this.route });
  }

  isFormChanged(): boolean {
    this.createActivity();
    if (
      this.oldActivity.title === this.newActivity.title &&
      this.oldActivity.description === this.newActivity.description &&
      new Date(this.oldActivity.dateTime).toISOString() ===
        this.newActivity.dateTime &&
      this.oldActivity.imageUrl === this.newActivity.imageUrl
    ) {
      return false;
    } else {
      return true;
    }
  }

  canDeactivate(): boolean | Observable<boolean> | Promise<boolean> {
    if (
      (!this.changesSaved && this.createMode) ||
      (!this.createMode && this.isFormChanged() && !this.changesSaved)
    ) {
      const dialogRef = this.matDialog.open(ConfirmationDialogComponent, {
        width: '350px',
        data: 'Changes you made may not be saved. Are you sure you want to discard them?',
      });

      return dialogRef.afterClosed().pipe(
        take(1),
        map((dialogResult) => !!dialogResult)
      );
    } else {
      return true;
    }
  }

  myFilter = (d: Date | null): boolean => {
    const today = new Date();
    today.setHours(0, 0, 0, 0);
  
    const dateToCompare = d ? new Date(d) : today;
    dateToCompare.setHours(0, 0, 0, 0);
  
    return dateToCompare.getTime() >= today.getTime();
  };
}
