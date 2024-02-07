import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PATH, WORKOUT_PATH } from '../constants/uri-paths';
import { Observable } from 'rxjs';
import { WorkoutViewDTO } from '../models/workout/workout-view-dto';
import { CLIENT } from '../constants/role-names';

@Injectable({
  providedIn: 'root',
})
export class WorkoutService {
  private path: string = PATH + WORKOUT_PATH;

  constructor(private http: HttpClient) {}

  getClientWorkouts(clientId: string): Observable<WorkoutViewDTO[]> {
    return this.http.get<WorkoutViewDTO[]>(
      this.path + '/' + CLIENT + '/' + clientId
    );
  }

  // getActivity(id: string): Observable<ActivityViewDTO> {
  //   return this.http.get<ActivityViewDTO>(PATH + ACTIVITY_PATH + '/' + id);
  // }

  // addActivity(activity: ActivityInDTO): Observable<void> {
  //   return this.http.post<any>(PATH + ACTIVITY_PATH, activity);
  // }

  // updateActivity(id: string, activity: ActivityInDTO): Observable<void> {
  //   return this.http.put<any>(PATH + ACTIVITY_PATH + '/' + id, activity);
  // }

  // deleteActivity(id: string): Observable<void> {
  //   return this.http.delete<any>(PATH + ACTIVITY_PATH + '/' + id);
  // }
}
