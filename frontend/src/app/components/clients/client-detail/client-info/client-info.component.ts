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
import { tap } from 'rxjs';
import { ADMIN } from 'src/app/constants/role-names';
import { ClientViewDTO } from 'src/app/models/client/client-view-dto';
import { AuthService } from 'src/app/services/auth.service';
import { ClientService } from 'src/app/services/client.service';

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

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService
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
  }

  initializeForm(): void {
    this.infoForm = this.formBuilder.group({
      firstName: [this.client.firstName, Validators.required],
      lastName: [this.client.lastName, Validators.required],
      birthday: [this.client.birthday, Validators.required],
      email: [this.client.email, [Validators.required, Validators.email]],
      goal: [this.client.goal],
      initialWeight: [this.client.initialWeight],
      initialHeight: [this.client.initialHeight],
    });
  }
}
