import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StateService {
  public clients$: Observable<void> = new Observable<void>();
  public programs$: Observable<void> = new Observable<void>();

  private clients: Subject<void> = new Subject<void>();
  private programs: Subject<void> = new Subject<void>();

  constructor() {
    this.clients$ = this.clients.asObservable();
    this.programs$ = this.programs.asObservable();
  }

  public clientsUpdated() {
    this.clients.next();
  }

  public programsUpdated() {
    this.programs.next();
  }
}
