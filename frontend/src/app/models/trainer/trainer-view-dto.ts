import { TrainerCategory } from "src/app/enums/trainer-category";

export interface TrainerViewDTO {
    id: string;
    firstName: string;
    lastName: string;
    category: TrainerCategory;
  }
  