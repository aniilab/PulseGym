import { Component, OnInit } from '@angular/core';
import {
  ActivatedRoute,
  Router,
  RouterLinkActive,
  RouterStateSnapshot,
} from '@angular/router';
import { tap } from 'rxjs';
import { ADMIN } from 'src/app/constants/role-names';
import { WorkoutType } from 'src/app/enums/workout-type';
import { ClientProgramViewDTO } from 'src/app/models/program/client-program-view-dto';
import { ProgramViewDTO } from 'src/app/models/program/program-view-dto';
import { AuthService } from 'src/app/services/auth.service';
import { ProgramService } from 'src/app/services/program.service';

@Component({
  selector: 'app-client-programs',
  templateUrl: './client-programs.component.html',
  styleUrls: ['./client-programs.component.css'],
})
export class ClientProgramsComponent implements OnInit {
  public activePrograms: ClientProgramViewDTO[] = [];
  public expiredPrograms: ClientProgramViewDTO[] = [];
  public workoutTypes = WorkoutType;
  public historyButton = {
    label: 'History',
    icon: 'keyboard_arrow_down',
    hidden: true,
  };

  public currentRole: string = '';
  public adminRole: string = ADMIN;

  public addProgram: boolean = false;
  public newProgram: ProgramViewDTO;
  public programs: ProgramViewDTO[] = [];

  public clientId: string = '';

  constructor(
    private programService: ProgramService,
    private route: ActivatedRoute,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.clientId = this.route.parent.snapshot.paramMap.get('id');

    this.authService.currentRole.subscribe((role) => (this.currentRole = role));
    this.fetchClientsPrograms();
    this.getAllPrograms();
  }

  fetchClientsPrograms(): void {
    this.programService
      .getClientPrograms(this.clientId)
      .pipe(
        tap((programs: ClientProgramViewDTO[]) => {
          this.activePrograms = programs
            .filter((program) => new Date(program.expirationDate) >= new Date())
            .sort(
              (a, b) =>
                new Date(a.expirationDate).getTime() -
                new Date(b.expirationDate).getTime()
            );
          this.expiredPrograms = programs
            .filter((program) => new Date(program.expirationDate) <= new Date())
            .sort(
              (a, b) =>
                new Date(a.expirationDate).getTime() -
                new Date(b.expirationDate).getTime()
            );
        })
      )
      .subscribe();
  }

  onShowHistory(): void {
    this.historyButton.hidden = !this.historyButton.hidden;
    if (this.historyButton.hidden) {
      this.historyButton.label = 'History';
      this.historyButton.icon = 'keyboard_arrow_down';
    } else {
      this.historyButton.label = 'Hide';
      this.historyButton.icon = 'keyboard_arrow_up';
    }
  }

  getAllPrograms(): void {
    this.programService.getAllPrograms().subscribe((programs) => {
      this.programs = programs;
    });
  }

  onAddProgram(): void {
    this.programService
      .addClientProgram(this.clientId, this.newProgram.id)
      .subscribe(() => {
        this.fetchClientsPrograms();
        this.addProgram = false;
      });
  }
}
