import { WorkoutRequestStatus } from "src/app/enums/workout-request-status";
import { ClientViewDTO } from "../client/client-view-dto";
import { TrainerViewDTO } from "../trainer/trainer-view-dto";

export interface WorkoutRequestViewDTO {
    id: string;
    dateTime: string;
    status: WorkoutRequestStatus;
    trainer: TrainerViewDTO;
    client: ClientViewDTO;
}