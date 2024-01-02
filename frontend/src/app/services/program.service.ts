import { Subject, Observable } from 'rxjs';
import { Program } from '../models/program.model';
import { Injectable } from '@angular/core';
import { MEMBERSHIP_PROGRAM_PATH, PATH } from '../constants/uri-paths';
import { HttpClient } from '@angular/common/http';
import { ProgramViewDTO } from '../models/program/program-view-dto';

@Injectable({
  providedIn: 'root',
})
export class ProgramService {
  private path: string = PATH + MEMBERSHIP_PROGRAM_PATH;

  constructor(private http: HttpClient) {}

  getAllPrograms(): Observable<ProgramViewDTO[]> {
    return this.http.get<ProgramViewDTO[]>(this.path);
  }
}
