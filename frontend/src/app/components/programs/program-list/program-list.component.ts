import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ProgramService } from 'src/app/services/program.service';
import { MatDialog } from '@angular/material/dialog';
import { ProgramCreateComponent } from '../program-create/program-create.component';
import { ProgramViewDTO } from 'src/app/models/program/program-view-dto';
import { ADMIN } from 'src/app/constants/role-names';
import { StateService } from 'src/app/services/state.service';

@Component({
  selector: 'app-program-list',
  templateUrl: './program-list.component.html',
  styleUrls: ['./program-list.component.css'],
})
export class ProgramListComponent implements OnInit {
  public programs: ProgramViewDTO[];
  public currentRole: string = '';
  public adminRole = ADMIN;

  constructor(
    private authService: AuthService,
    private matDialog: MatDialog,
    private programService: ProgramService,
    private stateService: StateService
  ) {}

  ngOnInit(): void {
    this.getProgramsList();

    this.authService.currentRole.subscribe((role: string) => {
      this.currentRole = role;
    });

    this.stateService.programs$.subscribe(()=>{
      this.getProgramsList();
    });
  }

  getProgramsList(): void {
    this.programService
      .getAllPrograms()
      .subscribe((programs: ProgramViewDTO[]) => (this.programs = programs));
  }

  openCreateDialog() {
    this.matDialog.open(ProgramCreateComponent, {
      width: '450px',
    });
  }
}
