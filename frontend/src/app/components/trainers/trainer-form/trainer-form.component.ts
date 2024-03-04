import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map, take, tap } from 'rxjs';
import { CanComponentDeactivate } from 'src/app/guards/deactivate-guard.service';
import { ClientInDTO } from 'src/app/models/client/client-in-dto';
import { ClientService } from 'src/app/services/client.service';
import { ConfirmationDialogComponent } from '../../dialogs/confirmation-dialog/confirmation-dialog.component';
import { TrainerCategory } from 'src/app/enums/trainer-category';
import { TrainerCreateDTO } from 'src/app/models/trainer/trainer-create-dto';
import { TrainerService } from 'src/app/services/trainer.service';

@Component({
  selector: 'app-trainer-form',
  templateUrl: './trainer-form.component.html',
  styleUrls: ['./trainer-form.component.css'],
})
export class TrainerFormComponent implements OnInit, CanComponentDeactivate {
  public trainerForm: FormGroup;

  public categories: (string | TrainerCategory)[];
  public trainerCategories = TrainerCategory;

  private newTrainer: TrainerCreateDTO;
  private changesSaved: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private trainerService: TrainerService,
    private formBuilder: FormBuilder,
    private matDialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.categories = Object.values(TrainerCategory).filter(value => typeof value === 'number');

    this.initializeForm();
  }

  initializeForm(): void {
    this.trainerForm = this.formBuilder.group({
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      birthday: [null, Validators.required],
      category: [null, Validators.required],
      email: [null, Validators.email],
      password: [null],
    });
  }

  onSubmit(): void {
    this.createTrainer();
    this.addTrainer();
  }

  private createTrainer(): void {
    const _firstName = this.trainerForm.get('firstName').value;
    const _lastName = this.trainerForm.get('lastName').value;
    const _birthday = new Date(this.trainerForm.get('birthday').value);
    const _birthdayString = new Date(
      _birthday.getTime() - _birthday.getTimezoneOffset() * 60000
    ).toISOString();
    const _category = this.trainerForm.get('category').value;
    const _email = this.trainerForm.get('email').value;
    const _password = this.trainerForm.get('password').value;

    this.newTrainer = {
      firstName: _firstName,
      lastName: _lastName,
      birthday: _birthdayString,
      category: _category,
      email: _email,
      password: _password
    } as TrainerCreateDTO;
  }

  addTrainer(): void {
    this.trainerService
      .addTrainer(this.newTrainer)
      .pipe(
        tap(() => {
          this.resetFormAndNavigate('..');
        })
      )
      .subscribe();
  }

  resetFormAndNavigate(relativePath: string): void {
    this.trainerForm.reset();
    this.changesSaved = true;
    this.router.navigate([relativePath], { relativeTo: this.route });
  }

  canDeactivate(): boolean | Observable<boolean> | Promise<boolean> {
    if (!this.changesSaved) {
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
}
