import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { tap } from 'rxjs';
import { ConfirmationDialogComponent } from 'src/app/components/dialogs/confirmation-dialog/confirmation-dialog.component';
import { ADMIN } from 'src/app/constants/role-names';
import { TrainerCategory } from 'src/app/enums/trainer-category';
import { Trainer } from 'src/app/models/trainer.model';
import { TrainerViewDTO } from 'src/app/models/trainer/trainer-view-dto';
import { AuthService } from 'src/app/services/auth.service';
import { StateService } from 'src/app/services/state.service';
import { TrainerService } from 'src/app/services/trainer.service';

@Component({
  selector: 'app-trainer',
  templateUrl: './trainer.component.html',
  styleUrls: ['./trainer.component.css'],
})
export class TrainerComponent implements OnInit {
  @Input()
  public trainer: TrainerViewDTO;
  public trainerCategories = TrainerCategory;
  public currentRole: string = '';
  public adminRole = ADMIN;

  constructor(
    private matDialog: MatDialog,
    private authService: AuthService,
    private trainerService: TrainerService,
    private stateService: StateService
  ) {}

  ngOnInit(): void {
    this.authService.currentRole.subscribe((role: string) => {
      this.currentRole = role;
    });
  }

  onDelete(): void {
    const dialogRef = this.matDialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: 'Are you sure that you want to delete this trainer?',
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      if (dialogResult) {
        this.trainerService
          .deleteTrainer(this.trainer.id)
          .subscribe(()=>{
            this.stateService.trainersUpdated();
          });
      }
    });
  }
}
