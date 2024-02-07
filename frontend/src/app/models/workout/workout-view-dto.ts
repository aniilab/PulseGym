import { WorkoutStatus } from 'src/app/enums/workout-status';
import { WorkoutType } from 'src/app/enums/workout-type';
import { TrainerViewDTO } from '../trainer/trainer-view-dto';
import { ClientViewDTO } from '../client/client-view-dto';
import { GroupClassViewDTO } from '../group-class/group-class-view-dto';

export interface WorkoutViewDTO {
  id: string;
  workoutDateTime: string;
  notes: string | null;
  status: WorkoutStatus;
  workoutType: WorkoutType;
  trainer: TrainerViewDTO | null;
  groupClass: GroupClassViewDTO | null;
  clients: ClientViewDTO[];
}
