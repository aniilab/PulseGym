import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Program } from 'src/app/models/program.model';
import { ProgramService } from 'src/app/services/program.service';
import { MatDialog } from '@angular/material/dialog';
import { ProgramCreateComponent } from '../program-create/program-create.component';
import { ProgramViewDTO } from 'src/app/models/program/program-view-dto';

@Component({
  selector: 'app-program-list',
  templateUrl: './program-list.component.html',
  styleUrls: ['./program-list.component.css'],
})
export class ProgramListComponent implements OnInit {
  public programs: ProgramViewDTO[];
  public currentRole: string = '';

  constructor(
    private authService: AuthService,
    private matDialog: MatDialog,
    private programService: ProgramService
  ) {}

  ngOnInit(): void {
    this.getProgramsList();

    // this.currentRole = this.authService.getRole();
    // this.authService.authRoleChanged.subscribe((role: string) => {
    //   this.currentRole = role;
    // });
  }

  getProgramsList(): void {
    this.programService
      .getAllPrograms()
      .subscribe((programs: ProgramViewDTO[]) => (this.programs = programs));
  }

  openCreateDialog() {
    this.matDialog.open(ProgramCreateComponent, {
      width: '350px',
    });
  }
}
