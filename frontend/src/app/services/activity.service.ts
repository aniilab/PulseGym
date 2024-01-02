import { Subject } from 'rxjs';
import { Activity } from '../models/activity.model';
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
  constructor(private http: HttpClient) {}

  getAllActivities(): Observable<ActivityViewDTO[]> {
    return this.http.get<ActivityViewDTO[]>(PATH + ACTIVITY_PATH);
  }

  getActivity(id: string): Observable<ActivityViewDTO> {
    return this.http.get<ActivityViewDTO>(PATH + ACTIVITY_PATH + '/' + id);
  }

  addActivity(activity: ActivityInDTO): Observable<void> {
    return this.http.post<any>(PATH + ACTIVITY_PATH, activity);
  }

  updateActivity(id: string, activity: ActivityInDTO): Observable<void> {
    return this.http.put<any>(PATH + ACTIVITY_PATH + '/' + id, activity);
  }

  deleteActivity(id: string): Observable<void> {
    return this.http.delete<any>(PATH + ACTIVITY_PATH + '/' + id);
  }
}
