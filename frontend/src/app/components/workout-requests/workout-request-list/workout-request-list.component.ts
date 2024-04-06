import { SelectionModel } from '@angular/cdk/collections';
import { Component } from '@angular/core';
import { tap } from 'rxjs';
import { ADMIN, CLIENT, TRAINER } from 'src/app/constants/role-names';
import { WorkoutRequestStatus } from 'src/app/enums/workout-request-status';
import { ClientViewDTO } from 'src/app/models/client/client-view-dto';
import { WorkoutRequestViewDTO } from 'src/app/models/workout-request/workout-request-view-dto';
import { AuthService } from 'src/app/services/auth.service';
import { ClientService } from 'src/app/services/client.service';
import { StateService } from 'src/app/services/state.service';
import { TrainerService } from 'src/app/services/trainer.service';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-workout-request-list',
  templateUrl: './workout-request-list.component.html',
  styleUrls: ['./workout-request-list.component.css']
})
export class WorkoutRequestListComponent {
  public currentRole: string = '';
  public trainerRole = TRAINER;
  public adminRole = ADMIN;

  public requests: WorkoutRequestViewDTO[] = [];
  public selection = new SelectionModel<WorkoutRequestViewDTO>(true, []);

  public displayedColumns: string[] = ['time', 'date', 'trainer', 'client', 'status', 'action'];
  public requestStatus = WorkoutRequestStatus;

  public selectedUserId: string = '';
  public selectedRole: string = '';

  public users: any[] = [];

  constructor(
    private workoutService: WorkoutService,
    private trainerService: TrainerService,
    private clientService: ClientService,
    private stateService: StateService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.stateService.requests$.subscribe(() => this.refreshData());

    this.authService.currentRole.subscribe((role) => {
      this.currentRole = role;
      if(this.currentRole != ADMIN) {
        this.authService.currentUser.subscribe((user) => {
          this.selectedRole = role;
          this.selectedUserId = user.id;
          this.fetchRequests();
        });
      }
    });
  }

  public onRoleSelected() {
    if(this.selectedRole == CLIENT) {
      this.clientService
      .getAllClients()
      .subscribe((clients) => (this.users = clients));
    }
    else{
      this.trainerService.getAllTrainers().pipe(
        tap((trainers) => {
          this.users = trainers;
        })
      ).subscribe();
    }
  }

  public onUserSelected() {
    this.fetchRequests();
  }

  public isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.requests.length;
    return numSelected === numRows;
  }

  public toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.requests);
  }

  public onAcceptClicked() {
    this.selection.selected.forEach(request => {
      this.workoutService.acceptWorkoutRequest(request.id).subscribe(() => {
        this.stateService.requestsUpdated();
      });
    });
  }

  public isDisabledCheckbox(row: WorkoutRequestViewDTO): boolean {
    return row.status != this.requestStatus.New;
  }

  public isDisabledHeaderCheckbox() {
    return !this.requests.some(request => request.status === this.requestStatus.New);
  }

  public onDeclineClicked() {
    this.selection.selected.forEach(request => {
      this.workoutService.declineWorkoutRequest(request.id).subscribe(() => {
        this.stateService.requestsUpdated();
      });
    });
  }

  private fetchRequests(): void {
      this.workoutService.getWorkoutRequests(this.selectedRole, this.selectedUserId).pipe(
        tap((requests) => {
          this.requests = requests;
        })
      ).subscribe();
  }

  private refreshData(): void {
    this.fetchRequests();
    this.selection.clear();
  }
}
