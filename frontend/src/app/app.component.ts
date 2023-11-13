import { Component, OnInit, HostListener } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isLoggedIn:boolean = false;

  constructor(private authService: AuthService){
  }
  ngOnInit(): void {
    this.authService.authChanged.subscribe(
      (isLogged)=>{
        this.isLoggedIn=isLogged;
      }
    );
  }


}
