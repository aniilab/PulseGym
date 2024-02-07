import { ClassLevel } from "src/app/enums/class-level";

export interface GroupClassViewDTO {
  id: string;
  name: string;
  description: string;
  maxClientNumber: number;
  level: ClassLevel;
}
