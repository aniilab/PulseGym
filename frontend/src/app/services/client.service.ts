import { Observable, tap} from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ClientViewDTO } from '../models/client/client-view-dto';
import { CLIENT_PATH, PATH } from '../constants/uri-paths';
import { ClientInDTO } from '../models/client/client-in-dto';

@Injectable()
export class ClientService {
  private path: string = PATH + CLIENT_PATH;

  constructor(private http: HttpClient) {}

  getAllClients(): Observable<ClientViewDTO[]> {
    return this.http.get<ClientViewDTO[]>(this.path).pipe(
      tap((clients: ClientViewDTO[])=>{
        clients.map(client=>{
          if (!client.imageUrl) {
            client.imageUrl = '../../assets/ava.jpg';
          }
        })
      })
    );
  }

  getClient(id: string): Observable<ClientViewDTO> {
    return this.http.get<ClientViewDTO>(this.path + '/' + id).pipe(
      tap((client: ClientViewDTO)=>{
          if (!client.imageUrl) {
            client.imageUrl = '../../assets/ava.jpg';
          }
        })
    );
  }

  addClient(client: ClientInDTO): Observable<void> {
    return this.http.post<any>(this.path, client);
  }

  // updateActivity(id: string, activity: ActivityInDTO): Observable<void> {
  //   return this.http.put<any>(PATH + ACTIVITY_PATH + '/' + id, activity);
  // }

  deleteClient(id: string): Observable<void> {
    return this.http.delete<any>(this.path + '/' + id);
  }
}
