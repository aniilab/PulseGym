import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-side-navbar',
  templateUrl: './side-navbar.component.html',
  styleUrls: ['./side-navbar.component.css'],
})
export class SideNavbarComponent implements OnInit {
  public role: string = '';

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.authRoleChanged.subscribe((role: string) => {
      this.role = role;
    });
  }
}
