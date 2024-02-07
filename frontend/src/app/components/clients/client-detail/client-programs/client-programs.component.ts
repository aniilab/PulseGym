import { Component, OnInit } from '@angular/core';
import {
  ActivatedRoute,
  Router,
  RouterLinkActive,
  RouterStateSnapshot,
} from '@angular/router';
import { tap } from 'rxjs';
import { WorkoutType } from 'src/app/enums/workout-type';
import { ClientProgramViewDTO } from 'src/app/models/program/client-program-view-dto';
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

  constructor(
    private programService: ProgramService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const clientId = this.route.parent.snapshot.paramMap.get('id');

    this.programService
      .getClientPrograms(clientId)
      .pipe(
        tap((programs: ClientProgramViewDTO[]) => {
          programs.map((program: ClientProgramViewDTO) => {
            if (
              new Date(program.expirationDate).getTime() > new Date().getTime()
            ) {
              this.expiredPrograms.push(program);
            } else {
              this.activePrograms.push(program);
            }
          });
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
}
