import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ACTIVITY_PATH, PATH } from '../constants/uri-paths';
import { ActivityViewDTO } from '../models/activity/activity-view-dto';
import { Observable } from 'rxjs';
import { ActivityInDTO } from '../models/activity/activity-in-dto';

@Injectable({
  providedIn: 'root',
})
export class ActivityService {
  private path: string = PATH + ACTIVITY_PATH;

  constructor(private http: HttpClient) {}

  getAllActivities(): Observable<ActivityViewDTO[]> {
    return this.http.get<ActivityViewDTO[]>(this.path);
  }

  getActivity(id: string): Observable<ActivityViewDTO> {
    return this.http.get<ActivityViewDTO>(this.path + '/' + id);
  }

  addActivity(activity: ActivityInDTO): Observable<void> {
    return this.http.post<any>(this.path, activity);
  }

  updateActivity(id: string, activity: ActivityInDTO): Observable<void> {
    return this.http.put<any>(this.path + '/' + id, activity);
  }

  deleteActivity(id: string): Observable<void> {
    return this.http.delete<any>(this.path + '/' + id);
  }
}
