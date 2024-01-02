import { Component } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Program } from 'src/app/models/program.model';
import { ProgramService } from 'src/app/services/program.service';

@Component({
  selector: 'app-program-create',
  templateUrl: './program-create.component.html',
  styleUrls: ['./program-create.component.css'],
})
export class ProgramCreateComponent {
  public createProgramForm: FormGroup;

  constructor(
    private programService: ProgramService,
    private formBuilder: FormBuilder,
  ) {}

  ngOnInit(): void {
    this.createProgramForm = this.formBuilder.group({
      name: [null, Validators.required],
      price: [0, Validators.required],
    });
  }

  onSubmit() {
    this.createProgram();

    this.createProgramForm.reset();
  }

  createProgram() {
    const _name = this.createProgramForm.get('name').value;
    const _price = this.createProgramForm.get('price').value;

    const program: Program = {
      Name: _name,
      Price: _price,
    };

  }
}
