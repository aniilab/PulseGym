import { Trainer } from './trainer.model';

export class Activity {
  constructor(
    public Title: string,
    public Description: string,
    public ImagePath: string,
    public Date: Date,
    public Trainer: Trainer | null
  ) {}
}

// export interface Activity {
//     Title: string;
//     Description: string;
//     ImagePath: string;
//     Date: Date;
//     Trainer: Trainer | null;
// }
