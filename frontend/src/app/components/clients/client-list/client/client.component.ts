import { Component, Input } from '@angular/core';
import { Client } from 'src/app/models/client.model';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent {
  @Input() 
  public client: Client;
}
