import { Injectable } from "@angular/core";
import { OPENAI_PATH, PATH } from "../constants/uri-paths";
import { HttpClient } from "@angular/common/http";
import { GeneratedWorkoutDTO } from "../models/assistant/generated-workout-dto";
import { Observable } from "rxjs";
import { GeneratedMealScheduleDTO } from "../models/assistant/generated-meal-schedule-dto";

@Injectable({
  providedIn: 'root',
})
export class OpenAIService {
  private path: string = PATH + OPENAI_PATH;

  constructor(private http: HttpClient) {}

  generateWorkouts(): Observable<GeneratedWorkoutDTO[]> {
    return this.http.get<GeneratedWorkoutDTO[]>(this.path + '/workout');
  }

  generateMeals(): Observable<GeneratedMealScheduleDTO[]> {
    return this.http.get<GeneratedMealScheduleDTO[]>(this.path + '/meal');
  }
}