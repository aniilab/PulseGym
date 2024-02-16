import { WorkoutType } from "src/app/enums/workout-type";

export interface MembershipProgramInDTO {
    name: string;
    price: number;
    duration: number;
    workoutType: WorkoutType;
    workoutNumber: number;
  }
  