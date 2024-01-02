import { WorkoutType } from 'src/app/enums/workout-type';

export interface ProgramViewDTO {
  id: string;
  name: string;
  price: number;
  duration: number;
  workoutType: WorkoutType;
  workoutNumber: number;
}
