import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-side-navbar',
  templateUrl: './side-navbar.component.html',
  styleUrls: ['./side-navbar.component.css'],
})
export class SideNavbarComponent implements OnInit {
  public isLoggedIn: boolean = false;
  public role: string = '';

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.isAuthenticated.subscribe((isLogged: boolean) => {
      this.isLoggedIn = isLogged;
    });

    this.authService.currentRole.subscribe((role: string) => {
      this.role = role;
    });
  }
}
