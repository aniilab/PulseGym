import { Component, OnInit } from '@angular/core';
import { ClientService } from 'src/app/services/client.service';
import { AuthService } from 'src/app/services/auth.service';
import { ClientViewDTO } from 'src/app/models/client/client-view-dto';
import { ADMIN } from 'src/app/constants/role-names';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css'],
})
export class ClientListComponent implements OnInit {
  public clients: ClientViewDTO[];
  public currentRole: string = '';
  public adminRole: string = ADMIN;
  public searchText: string = "";

  constructor(
    private authService: AuthService,
    private clientService: ClientService
  ) {}

  ngOnInit(): void {
    this.getClients();

    this.authService.currentRole.subscribe((role: string) => {
      this.currentRole = role;
    });
  }

  getClients(): void {
    this.clientService
      .getAllClients()
      .subscribe((clients: ClientViewDTO[]) => (this.clients = clients));
  }
}
