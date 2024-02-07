import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map, take, tap } from 'rxjs';
import { CanComponentDeactivate } from 'src/app/guards/deactivate-guard.service';
import { ClientInDTO } from 'src/app/models/client/client-in-dto';
import { ClientService } from 'src/app/services/client.service';
import { ConfirmationDialogComponent } from '../../dialogs/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html',
  styleUrls: ['./client-form.component.css'],
})
export class ClientFormComponent implements OnInit, CanComponentDeactivate {
  public clientForm: FormGroup;

  private newClient: ClientInDTO;
  private changesSaved: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private clientService: ClientService,
    private formBuilder: FormBuilder,
    private matDialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(): void {
    this.clientForm = this.formBuilder.group({
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      birthday: [null, Validators.required],
      email: [null, Validators.email],
      password: [null],
    });
  }

  onSubmit(): void {
    this.createClient();
    this.addClient();
  }

  createClient(): void {
    const _firstName = this.clientForm.get('firstName').value;
    const _lastName = this.clientForm.get('lastName').value;
    const _birthday = new Date(this.clientForm.get('birthday').value);
    const _birthdayString = new Date(
      _birthday.getTime() - _birthday.getTimezoneOffset() * 60000
    ).toISOString();
    const _email = this.clientForm.get('email').value;
    const _password = this.clientForm.get('password').value;

    this.newClient = new ClientInDTO(
      _firstName,
      _lastName,
      _birthdayString,
      _email,
      _password
    );
  }

  addClient(): void {
    this.clientService
      .addClient(this.newClient)
      .pipe(
        tap(() => {
          this.resetFormAndNavigate('..');
        })
      )
      .subscribe();
  }

  resetFormAndNavigate(relativePath: string): void {
    this.clientForm.reset();
    this.changesSaved = true;
    this.router.navigate([relativePath], { relativeTo: this.route });
  }

  canDeactivate(): boolean | Observable<boolean> | Promise<boolean> {
    if (!this.changesSaved) {
      const dialogRef = this.matDialog.open(ConfirmationDialogComponent, {
        width: '350px',
        data: 'Changes you made may not be saved. Are you sure you want to discard them?',
      });

      return dialogRef.afterClosed().pipe(
        take(1),
        map((dialogResult) => !!dialogResult)
      );
    } else {
      return true;
    }
  }
}
