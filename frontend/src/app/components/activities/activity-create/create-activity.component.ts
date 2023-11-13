import { Component } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Activity } from 'src/app/models/activity.model';
import { ActivityService } from 'src/app/services/activity.service';
import { TrainerService } from 'src/app/services/trainer.service';

@Component({
  selector: 'app-activity-create',
  templateUrl: './activity-create.component.html',
  styleUrls: ['./activity-create.component.css'],
})
export class ActivityCreateComponent {
  public createActivityForm: FormGroup;

  constructor(
    private activityService: ActivityService,
    private formBuilder: FormBuilder,
    private router: Router,
    private trainerService: TrainerService
  ) {}

  ngOnInit(): void {
    this.createActivityForm = this.formBuilder.group({
      title: [null, Validators.required],
      description: [null, Validators.required],
      imageUrl: [null, Validators.required],
      date: [Date.now, Validators.required],
      time: [null],
      trainerId: [null],
    });
  }

  onSubmit() {
    this.createActivity();

    this.createActivityForm.reset();
    this.router.navigate(['/']);
  }

  createActivity() {
    const _title = this.createActivityForm.get('title').value;
    const _description = this.createActivityForm.get('description').value;
    const _imageUrl = this.createActivityForm.get('imageUrl').value;
    const _dateString = this.createActivityForm.get('date').value;
    const _date = new Date(_dateString);
    const _trainerId = this.createActivityForm.get('trainerId').value;

    const activity: Activity = {
      Title: _title,
      Description: _description,
      ImagePath: _imageUrl,
      Date: _date,
      Trainer: this.trainerService.getTrainer(_trainerId),
    };

    this.activityService.addActivity(activity);
  }
}
