import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/components/dialogs/confirmation-dialog/confirmation-dialog.component';
import { ADMIN } from 'src/app/constants/role-names';
import { WorkoutType } from 'src/app/enums/workout-type';
import { ProgramViewDTO } from 'src/app/models/program/program-view-dto';
import { AuthService } from 'src/app/services/auth.service';
import { ProgramService } from 'src/app/services/program.service';
import { StateService } from 'src/app/services/state.service';

@Component({
  selector: 'app-program',
  templateUrl: './program.component.html',
  styleUrls: ['./program.component.css'],
})
export class ProgramComponent implements OnInit {
  @Input()
  public program: ProgramViewDTO;
  public workoutTypes = WorkoutType;
  public adminRole = ADMIN;
  public currentRole = '';

  constructor(private authService: AuthService, private programService: ProgramService, private stateService: StateService, private matDialog: MatDialog) {}

  ngOnInit(): void {
    this.authService.currentRole.subscribe((role) => (this.currentRole = role));
  }

  onDelete(): void {
    const dialogRef = this.matDialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: 'Are you sure that you want to delete this program?',
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      if (dialogResult) {
        this.programService.deleteProgram(this.program.id).subscribe(() => this.stateService.programsUpdated());
      }
    });
  }
}
