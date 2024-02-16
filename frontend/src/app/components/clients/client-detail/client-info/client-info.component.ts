import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChange,
  SimpleChanges,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route } from '@angular/router';
import { finalize, tap } from 'rxjs';
import { ADMIN } from 'src/app/constants/role-names';
import { ClientViewDTO } from 'src/app/models/client/client-view-dto';
import { ClientUpdateDTO } from 'src/app/models/client/client-update-dto';
import { AuthService } from 'src/app/services/auth.service';
import { ClientService } from 'src/app/services/client.service';
import { StateService } from 'src/app/services/state.service';

@Component({
  selector: 'app-client-info',
  templateUrl: './client-info.component.html',
  styleUrls: ['./client-info.component.css'],
})
export class ClientInfoComponent implements OnInit {
  public client: ClientViewDTO = {} as ClientViewDTO;
  public currentRole: string = '';
  public infoForm: FormGroup;
  public editMode: boolean = false;

  public AdminRole: string = ADMIN;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private clientService: ClientService,
    private stateService: StateService
  ) {}

  ngOnInit(): void {
    this.initializeForm();

    this.authService.currentRole
      .pipe(
        tap((role) => {
          this.currentRole = role;
          if (this.currentRole != ADMIN) {
            this.infoForm.disable();
          }
        })
      )
      .subscribe();

      this.stateService.clients$.subscribe(
        () => {
          this.getClientInfo();
        }
      );
  }

  initializeForm(): void {
    this.infoForm = this.formBuilder.group({
      firstName: [this.client.firstName, Validators.required],
      lastName: [this.client.lastName, Validators.required],
      birthday: [this.client.birthday, Validators.required],
      email: [this.client.email, [Validators.required, Validators.email]],
      imageUrl: [this.client.imageUrl === "../../assets/ava.jpg" ? null : this.client.imageUrl],
      goal: [this.client.goal],
      initialWeight: [this.client.initialWeight],
      initialHeight: [this.client.initialHeight],
    });
  }

  private getClientInfo(): void {
    this.clientService.getClient(this.client.id).subscribe((client: ClientViewDTO) => {
      this.client = client;
      this.initializeForm();
      this.editMode = false;
    });
  }

  onInputClick(): void {
    this.editMode = true;
  }

  onSubmit(): void {
    this.updateClientInfo();
  }

  onSlide(checked: boolean): void {
    if(!checked) this.initializeForm();
  }

  private updateClientInfo(): void {
    const _firstName = this.infoForm.get('firstName').value;
    const _lastName = this.infoForm.get('lastName').value;
    const _birthday = new Date(this.infoForm.get('birthday').value);
    const _birthdayString = new Date(_birthday.getTime() - (_birthday.getTimezoneOffset() * 60000)).toISOString();
    const _imageUrl = this.infoForm.get('imageUrl').value;
    const _goal = this.infoForm.get('goal').value; 
    const _initialWeight = this.infoForm.get('initialWeight').value; 
    const _initialHeight = this.infoForm.get('initialHeight').value; 

    const updatedClient = {
      firstName: _firstName,
      lastName: _lastName,
      birthday: _birthdayString,
      imageUrl: _imageUrl,
      goal: _goal,
      initialHeight: _initialHeight,
      initialWeight: _initialWeight
    } as ClientUpdateDTO;

    this.clientService.updateClient(this.client.id, updatedClient).pipe(
      finalize(() => {
        this.stateService.clientsUpdated();
      })
    ).subscribe();
  }
}
