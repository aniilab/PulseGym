import { Subject, Observable } from 'rxjs';
import { Program } from '../models/program.model';
import { Injectable } from '@angular/core';

@Injectable()
export class ProgramService {
  public programsChanged = new Subject<Program[]>();

  private programs: Program[] = [
    new Program('basic', 600),
    new Program('super', 900),
    new Program('premium', 1300),
  ];

  constructor() {}

  getPrograms(): Program[] {
    return this.programs.slice();
  }

  getProgram(id: number): Program {
    return this.programs.at(id);
  }

  addProgram(newProgram: Program) {
    this.programs.push(newProgram);
    this.programsChanged.next(this.programs);
  }
}
