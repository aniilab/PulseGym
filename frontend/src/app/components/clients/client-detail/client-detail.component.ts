import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { tap } from 'rxjs';
import { ADMIN, CLIENT, TRAINER } from 'src/app/constants/role-names';
import { ClientViewDTO } from 'src/app/models/client/client-view-dto';
import { AuthService } from 'src/app/services/auth.service';
import { ClientService } from 'src/app/services/client.service';
import { ClientInfoComponent } from './client-info/client-info.component';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../dialogs/confirmation-dialog/confirmation-dialog.component';
import { StateService } from 'src/app/services/state.service';
import { TrainerViewDTO } from 'src/app/models/trainer/trainer-view-dto';
import { TrainerService } from 'src/app/services/trainer.service';
import { FormControl } from '@angular/forms';
import { TrainerCategory } from 'src/app/enums/trainer-category';

@Component({
  selector: 'app-client-detail',
  templateUrl: './client-detail.component.html',
  styleUrls: ['./client-detail.component.css'],
})
export class ClientDetailComponent implements OnInit {
  public client: ClientViewDTO;
  public currentRole: string = '';
  public navLinks: any[] = [];
  public adminRole: string = ADMIN;

  public trainers: TrainerViewDTO[] = [];
  public trainerChange: boolean = false;
  public personalTrainerControl: FormControl<TrainerViewDTO | null>;
  public personalTrainer: TrainerViewDTO | null;

  constructor(
    private authService: AuthService,
    private clientService: ClientService,
    private route: ActivatedRoute,
    private matDialog: MatDialog,
    private router: Router,
    private stateService: StateService,
    private trainerService: TrainerService
  ) {}

  ngOnInit(): void {
    this.getClient();
    this.getTrainers();

    this.authService.currentRole.subscribe((role: string) => {
      this.currentRole = role;
      this.setNavLinks();
    });

    this.stateService.clients$.subscribe(() => {
      this.getClient();
    });
  }

  setNavLinks() {
    this.navLinks.push({
      label: 'Info',
      link: 'info',
      index: 1,
    });

    if (this.currentRole === ADMIN) {
      this.navLinks.push({ label: 'Programs', link: 'programs', index: 2 });
      this.navLinks.push({ label: 'Workouts', link: 'workouts', index: 3 });
    } else if (this.currentRole === TRAINER) {
      this.navLinks.push({ label: 'Workouts', link: 'workouts', index: 0 });
    }

    this.navLinks.sort((a, b) => a.index - b.index);
  }

  getClient(): void {
    const clientId = this.route.snapshot.paramMap.get('id');

    this.clientService
      .getClient(clientId)
      .pipe(tap((client: ClientViewDTO) => {
        this.client = client;
        if(!!client.personalTrainer){
          this.personalTrainer = client.personalTrainer;
        } 
      })).subscribe();
  }

  getTrainers(): void {
    this.trainerService
      .getAllTrainers()
      .pipe(tap((trainers: TrainerViewDTO[]) => (this.trainers = trainers)))
      .subscribe();
  }

  onSetTrainer(): void {
    const newTrainerId = !!this.personalTrainer ? this.personalTrainer.id : null;

    this.clientService
    .addClientsTrainer(this.client.id, newTrainerId)
    .pipe(tap(() => {
      this.getClient();
      this.trainerChange = false;
    }))
    .subscribe();
  }

  onOutletLoaded(component) {
    if (component.client) {
      component.client = this.client;
    }
  }

  onDelete() {
    const dialogRef = this.matDialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: 'Are you sure that you want to delete this client?',
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      if (dialogResult) {
        this.clientService
          .deleteClient(this.client.id)
          .pipe(tap(() => this.router.navigate(['/clients'])))
          .subscribe();
      }
    });
  }
}
