import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StateService {
  public clients$: Observable<void> = new Observable<void>();
  public programs$: Observable<void> = new Observable<void>();
  public trainers$: Observable<void> = new Observable<void>();
  public requests$: Observable<void> = new Observable<void>();

  private clients: Subject<void> = new Subject<void>();
  private programs: Subject<void> = new Subject<void>();
  private trainers: Subject<void> = new Subject<void>();
  private requests: Subject<void> = new Subject<void>();

  constructor() {
    this.clients$ = this.clients.asObservable();
    this.programs$ = this.programs.asObservable();
    this.trainers$ = this.trainers.asObservable();
    this.requests$ = this.requests.asObservable();
  }

  public clientsUpdated() {
    this.clients.next();
  }

  public programsUpdated() {
    this.programs.next();
  }

  public trainersUpdated() {
    this.trainers.next();
  }

  public requestsUpdated() {
    this.requests.next();
  }
}
