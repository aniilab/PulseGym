import { Component, Input } from '@angular/core';
import { ClientViewDTO } from 'src/app/models/client/client-view-dto';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css'],
})
export class ClientComponent {
  @Input()
  public client: ClientViewDTO;
}
