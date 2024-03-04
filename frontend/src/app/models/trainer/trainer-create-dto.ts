import { TrainerCategory } from "src/app/enums/trainer-category";

export interface TrainerCreateDTO {
    firstName: string;
    lastName: string;
    category: TrainerCategory;
    birthday: string;
    email: string;
    password: string;
  }