import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LoginDialogComponent } from '../../login-dialog/login-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  public currentRole: string = '';
  public isLoggedIn: boolean = false;

  constructor(
    private authService: AuthService,
    private matDialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.authChanged.subscribe((isLogged) => {
      this.isLoggedIn = isLogged;
    });

    this.authService.authRoleChanged.subscribe((role: string) => {
      this.currentRole = role;
    });
  }

  openDialog() {
    this.matDialog.open(LoginDialogComponent, {
      width: '350px',
    });
  }

  onLogOut() {
    this.authService.logOut();
    this.router.navigate(['/']);
  }
}
