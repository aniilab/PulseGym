import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css'],
})
export class LoginDialogComponent implements OnInit {
  public loginForm: FormGroup;
  public successfulLogin: boolean = true;

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private matDialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    const email = this.loginForm.get('email').value;
    const password = this.loginForm.get('password').value;

    this.login(email, password);
  }

  login(email: string, password: string) {
    this.authService.login(email, password).subscribe((result) => {
      this.successfulLogin = result;
      if (result) {
        this.matDialog.closeAll();
      }
    });
  }
}
