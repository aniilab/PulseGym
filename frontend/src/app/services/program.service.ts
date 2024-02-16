import { Subject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { MEMBERSHIP_PROGRAM_PATH, PATH } from '../constants/uri-paths';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ProgramViewDTO } from '../models/program/program-view-dto';
import { ClientProgramViewDTO } from '../models/program/client-program-view-dto';
import { MembershipProgramInDTO } from '../models/program/membership-program-in-dto';

@Injectable({
  providedIn: 'root',
})
export class ProgramService {
  private path: string = PATH + MEMBERSHIP_PROGRAM_PATH;

  constructor(private http: HttpClient) {}

  getAllPrograms(): Observable<ProgramViewDTO[]> {
    return this.http.get<ProgramViewDTO[]>(this.path);
  }

  addClientProgram(clientId: string, programId: string): Observable<void> {
    const params = new HttpParams().set('programId', programId);
    return this.http.post<any>(this.path + '/Client/' + clientId, null, {
      params,
    });
  }

  getClientPrograms(clientId: string): Observable<ClientProgramViewDTO[]> {
    return this.http.get<ClientProgramViewDTO[]>(
      this.path + '/Client/' + clientId
    );
  }

  addProgram(programDto: MembershipProgramInDTO): Observable<void> {
    return this.http.post<any>(this.path, programDto);
  }

  deleteProgram(programId: string): Observable<void> {
    return this.http.delete<any>(this.path + '/' + programId);
  }
}
