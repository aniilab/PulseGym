import { TrainerViewDTO } from "../trainer/trainer-view-dto";

export interface ClientViewDTO {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    imageUrl: string | null;
    goal: string | null;
    birthday: string;
    initialWeight: number | null;
    initialHeight: number | null;
    personalTrainer: TrainerViewDTO;
  }
  