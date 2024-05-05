import { Component, OnInit, ViewChild } from '@angular/core';
import { EventInput } from '@fullcalendar/core';
import interactionPlugin from '@fullcalendar/interaction';
import timeGridPlugin from '@fullcalendar/timegrid';
import { ADMIN, CLIENT, TRAINER } from 'src/app/constants/role-names';
import { AuthService } from 'src/app/services/auth.service';
import { ClientService } from 'src/app/services/client.service';
import { TrainerService } from 'src/app/services/trainer.service';
import { tap } from 'rxjs';
import { WorkoutViewDTO } from 'src/app/models/workout/workout-view-dto';
import { WorkoutService } from 'src/app/services/workout.service';
import { FullCalendarComponent } from '@fullcalendar/angular';
import { WorkoutCreateComponent } from './workout-create/workout-create.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css'],
})
export class ScheduleComponent implements OnInit {
  @ViewChild('fullcalendar')
  public fullcalendar?: FullCalendarComponent;

  public currentRole: string = '';
  public trainerRole = TRAINER;
  public adminRole = ADMIN;

  public selectedUserId: string = '';
  public selectedFilter: string = '';

  public users: any[] = [];

  public showGroups: boolean = true;

  calendarOptions = {
    plugins: [interactionPlugin, timeGridPlugin],
    headerToolbar: {
      left: 'prev,next today addEvent',
      center: 'title',
      right: 'timeGridWeek,timeGridDay',
    },
    initialView: 'timeGridWeek',
    customButtons: {
      addEvent: {
        text: 'Create',
        click: function () {
          this.matDialog.open(WorkoutCreateComponent, {
            width: '450px',
          });
        },
      },
    },
    weekends: true,
    editable: false,
    selectable: false,
    selectMirror: true,
    dayMaxEvents: true,
    /* you can update a remote database when these fire:
    eventAdd:
    eventChange:
    eventRemove:
    */
  };

  constructor(
    private authService: AuthService,
    private clientService: ClientService,
    private trainerService: TrainerService,
    private workoutService: WorkoutService,
    private matDialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.authService.currentRole.subscribe((role) => {
      this.currentRole = role;
      if (this.currentRole != ADMIN) {
        this.authService.currentUser.subscribe((user) => {
          this.selectedUserId = user.id;
          this.selectedFilter = 'User';
          this.fetchUserWorkouts();
        });
      } else {
        this.selectedFilter = 'Group';
        this.fetchGroupWorkouts();
      }
    });
  }

  public onFilterSelected() {
    this.fullcalendar.events = [];
    if (this.selectedFilter === CLIENT) {
      this.clientService
        .getAllClients()
        .subscribe((clients) => (this.users = clients));
    } else if (this.selectedFilter === TRAINER) {
      this.trainerService
        .getAllTrainers()
        .pipe(
          tap((trainers) => {
            this.users = trainers;
          })
        )
        .subscribe();
    } else if (this.selectedFilter === 'Group') {
      this.fetchGroupWorkouts();
    } else if (this.selectedFilter === 'User') {
      this.fetchUserWorkouts();
    }
  }

  public onUserSelected() {
    this.fullcalendar.events = [];

    this.fetchUserWorkouts();
  }

  private fetchUserWorkouts(): void {
    if (this.selectedFilter === 'Group') return;

    if (this.selectedFilter === 'User') {
      this.workoutService
        .getUserWorkouts(this.currentRole, this.selectedUserId)
        .pipe(
          tap((workouts) => {
            this.fullcalendar.events = workouts.map((workout) => {
              return {
                title: workout.groupClass.name,
                start: new Date(workout.workoutDateTime),
                end: new Date(
                  new Date(workout.workoutDateTime).setHours(
                    new Date(workout.workoutDateTime).getHours() + 1
                  )
                ),
              };
            });
          })
        )
        .subscribe();
    } else {
      this.workoutService
        .getUserWorkouts(this.selectedFilter, this.selectedUserId)
        .pipe(
          tap((workouts) => {
            this.fullcalendar.events = workouts.map((workout) => {
              return {
                title: workout.groupClass.name,
                start: new Date(workout.workoutDateTime),
                end: new Date(
                  new Date(workout.workoutDateTime).setHours(
                    new Date(workout.workoutDateTime).getHours() + 1
                  )
                ),
              };
            });
          })
        )
        .subscribe();
    }
  }

  private fetchGroupWorkouts(): void {
    var curr = new Date();
    curr.setHours(0, 0, 0, 0);
    var first = curr.getDate() - curr.getDay();
    var last = first + 6;

    var firstday = new Date(curr.setDate(first));
    var lastday = new Date(curr.setDate(last));

    lastday.setHours(23, 59, 59, 99);

    this.workoutService
      .getGroupWorkouts(firstday.toUTCString(), lastday.toUTCString())
      .pipe(
        tap((workouts) => {
          this.fullcalendar.events = workouts.map((workout) => {
            return {
              title: workout.groupClass.name,
              start: new Date(workout.workoutDateTime),
              end: new Date(
                new Date(workout.workoutDateTime).setHours(
                  new Date(workout.workoutDateTime).getHours() + 1
                )
              ),
            };
          });
        })
      )
      .subscribe();
  }

  private openCreateDialog() {

  }
}
