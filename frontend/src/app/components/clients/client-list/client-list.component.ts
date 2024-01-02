import { Component, OnInit } from '@angular/core';
import { Client } from 'src/app/models/client.model';
import { Trainer } from 'src/app/models/trainer.model';
import { ClientService } from 'src/app/services/client.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css']
})
export class ClientListComponent implements OnInit {
  public clients: Client[];
  public currentRole: string = '';

  constructor(
    private authService: AuthService,
    private clientService: ClientService
  ) {}

  ngOnInit(): void {
    this.clients = this.clientService.getClients();

    // this.currentRole = this.authService.getRole();
    // this.authService.authRoleChanged.subscribe((role: string) => {
    //   this.currentRole = role;
    // });
  }
}
