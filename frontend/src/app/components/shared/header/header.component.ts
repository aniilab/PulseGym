import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LoginDialogComponent } from '../../dialogs/login-dialog/login-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { UserLoginResponseDTO } from 'src/app/models/user/user-login-response-dto';
import { ConfirmationDialogComponent } from '../../dialogs/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  public isLoggedIn: boolean = false;
  public currentUser: UserLoginResponseDTO;

  constructor(
    private authService: AuthService,
    private matDialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.isAuthenticated.subscribe((isLogged) => {
      this.isLoggedIn = isLogged;
    });

    this.authService.currentUser.subscribe((user: UserLoginResponseDTO) => {
      this.currentUser = user;
    });
  }

  openDialog() {
    this.matDialog.open(LoginDialogComponent, {
      width: '350px',
    });
  }

  onLogOut() {
    const dialogRef = this.matDialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: 'Are you sure that you want to log out?',
    });

    dialogRef.afterClosed().subscribe((dialogResult) => {
      if (dialogResult) {
        this.authService.logOut().subscribe();
        this.router.navigate(['/']);
      }
    });
  }
}
