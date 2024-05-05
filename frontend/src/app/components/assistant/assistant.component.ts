import { Component, OnInit } from '@angular/core';
import { concatMap, finalize, switchMap, tap } from 'rxjs';
import { GeneratedMealScheduleDTO } from 'src/app/models/assistant/generated-meal-schedule-dto';
import { GeneratedWorkoutDTO } from 'src/app/models/assistant/generated-workout-dto';
import { ClientViewDTO } from 'src/app/models/client/client-view-dto';
import { UserLoginResponseDTO } from 'src/app/models/user/user-login-response-dto';
import { AuthService } from 'src/app/services/auth.service';
import { ClientService } from 'src/app/services/client.service';
import { OpenAIService } from 'src/app/services/openai.service';

@Component({
  selector: 'app-assistant',
  templateUrl: './assistant.component.html',
  styleUrls: ['./assistant.component.css'],
})
export class AssistantComponent implements OnInit {
  public client: ClientViewDTO;
  public workouts: GeneratedWorkoutDTO[] = [];
  public meals: GeneratedMealScheduleDTO[] = [];

  public wLoading: boolean = false;
  public mLoading: boolean = false;
  constructor(
    private clientService: ClientService,
    private authService: AuthService,
    private openaiService: OpenAIService
  ) {}

  ngOnInit(): void {
    this.getClient();

    let workouts = localStorage.getItem("ai_generated_workouts");
    if(!!workouts && workouts.length) {
      this.workouts = JSON.parse(workouts);
    }

    let meals = localStorage.getItem("ai_generated_meals");
    if(!!meals && meals.length) {
      this.meals = JSON.parse(meals);
    }
  }

  getClient(): void {
    this.authService.currentUser
      .pipe(switchMap((user) => this.clientService.getClient(user.id)))
      .subscribe((client) => {
        this.client = client;
      });
  }

  generateWorkout(): void {
    this.wLoading = true;
    this.openaiService
      .generateWorkouts()
      .pipe(
        tap((workouts) => {
          this.workouts = workouts;
          localStorage.setItem("ai_generated_workouts", JSON.stringify(workouts));
        }),
        finalize(() => (this.wLoading = false))
      )
      .subscribe();
  }

  generateMeal(): void {
    this.mLoading = true;
    this.openaiService
      .generateMeals()
      .pipe(
        tap((meals) => {
          this.meals = meals;
          localStorage.setItem("ai_generated_meals", JSON.stringify(meals));
        }),
        finalize(() => (this.mLoading = false))
      )
      .subscribe();
  }
}
