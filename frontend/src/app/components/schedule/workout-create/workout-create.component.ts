import { Component } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { WorkoutType } from 'src/app/enums/workout-type';
import { MembershipProgramInDTO } from 'src/app/models/program/membership-program-in-dto';
import { ProgramService } from 'src/app/services/program.service';
import { StateService } from 'src/app/services/state.service';

@Component({
  selector: 'app-workout-create',
  templateUrl: './workout-create.component.html',
  styleUrls: ['./workout-create.component.css'],
})
export class WorkoutCreateComponent {
  public createProgramForm: FormGroup;
  public workoutTypes: (string | WorkoutType)[];
  public workoutTypeEnum = WorkoutType;

  constructor(
    private programService: ProgramService,
    private formBuilder: FormBuilder,
    private stateService: StateService
  ) {}

  ngOnInit(): void {
    this.workoutTypes = Object.values(WorkoutType).filter(value => typeof value === 'number');

    this.createProgramForm = this.formBuilder.group({
      name: [null, Validators.required],
      workoutType: [null, Validators.required],
      duration: [null, Validators.required],
      workoutNumber: [null, Validators.required],
      price: [null, Validators.required],
    });
  }

  onSubmit() {
    this.createProgram();
  }

  createProgram() {
    const program = {
      name: this.createProgramForm.get('name').value,
      price: this.createProgramForm.get('price').value,
      duration: this.createProgramForm.get('duration').value,
      workoutType: this.createProgramForm.get('workoutType').value,
      workoutNumber: this.createProgramForm.get('workoutNumber').value
    } as MembershipProgramInDTO;

    this.programService.addProgram(program).pipe(
      tap(() => {
        this.createProgramForm.reset();
        this.stateService.programsUpdated();
      },
      error => {
        console.log(error.error);
      }),
    ).subscribe();

  }
}
