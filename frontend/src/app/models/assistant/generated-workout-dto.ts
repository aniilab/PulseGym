import { GeneratedExerciseDTO } from './generated-exercise-dto';

export interface GeneratedWorkoutDTO {
  weekDay: string;
  workoutType: string;
  workoutDuration: number;
  exercises: GeneratedExerciseDTO[];
}
