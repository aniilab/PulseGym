import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.css'],
})
export class LoginDialogComponent implements OnInit{
  public loginForm: FormGroup;
  public successfulLogin: boolean = true;

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['admin@gmail.com', [Validators.required, Validators.email]],
      password: ['admin123', Validators.required],
    });
  }

  onSubmit() {
    this.login();

    if (this.successfulLogin) {
      this.loginForm.reset();
    }
  }

  login() {
    this.successfulLogin = false;
    const email = this.loginForm.get('email').value;
    const password = this.loginForm.get('password').value;
    this.successfulLogin = this.authService.logIn(email, password);
  }
}
