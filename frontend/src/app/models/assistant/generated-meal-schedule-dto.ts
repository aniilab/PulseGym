import { GeneratedMealDTO } from "./generated-meal-dto";

export interface GeneratedMealScheduleDTO {
    weekDay: string;
    meals: GeneratedMealDTO[];
  }