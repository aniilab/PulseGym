import { WorkoutType } from 'src/app/enums/workout-type';

export interface ClientProgramViewDTO {
  id: string;
  name: string;
  workoutType: WorkoutType;
  workoutRemainder: number;
  expirationDate: string;
}
