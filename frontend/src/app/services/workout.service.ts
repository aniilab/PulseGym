import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PATH, WORKOUT_PATH } from '../constants/uri-paths';
import { Observable } from 'rxjs';
import { WorkoutViewDTO } from '../models/workout/workout-view-dto';
import { CLIENT } from '../constants/role-names';
import { WorkoutRequestViewDTO } from '../models/workout-request/workout-request-view-dto';

@Injectable({
  providedIn: 'root',
})
export class WorkoutService {
  private path: string = PATH + WORKOUT_PATH;

  constructor(private http: HttpClient) {}

  getUserWorkouts(role: string, clientId: string): Observable<WorkoutViewDTO[]> {
    return this.http.get<WorkoutViewDTO[]>(
      this.path + '/' + role + '/' + clientId
    );
  }

  getGroupWorkouts(from: string, to: string): Observable<WorkoutViewDTO[]> {
    const params = new HttpParams().set('dateFrom', from).set('dateTo', to);

    return this.http.get<WorkoutViewDTO[]>(this.path + '/Group', { params });
  }

  getWorkoutRequests(
    role: string,
    clientId: string
  ): Observable<WorkoutRequestViewDTO[]> {
    return this.http.get<WorkoutRequestViewDTO[]>(
      this.path + '/Requests/' + role + '/' + clientId
    );
  }

  acceptWorkoutRequest(id: string): Observable<void> {
    return this.http.put<any>(this.path + '/AcceptRequest/' + id, null);
  }

  declineWorkoutRequest(id: string): Observable<void> {
    return this.http.delete<any>(this.path + '/DeclineRequest/' + id);
  }

  // getActivity(id: string): Observable<ActivityViewDTO> {
  //   return this.http.get<ActivityViewDTO>(PATH + ACTIVITY_PATH + '/' + id);
  // }

  // addActivity(activity: ActivityInDTO): Observable<void> {
  //   return this.http.post<any>(PATH + ACTIVITY_PATH, activity);
  // }

  // deleteActivity(id: string): Observable<void> {
  //   return this.http.delete<any>(PATH + ACTIVITY_PATH + '/' + id);
  // }
}
