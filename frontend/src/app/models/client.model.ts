import { Trainer } from './trainer.model';
import { Program } from './program.model';
import { UserRequest } from './userRequest.model';

export class Client {
  constructor(
    public User: UserRequest,
    public FirstName: string,
    public LastName: string,
    public Birthday: Date | null,
    public ImagePath: string,
    public Program: Program | null,
    public Goal: string,
    public InitialWeight: number,
    public CurrentWeight: number,
    public Height: number,
    public PersonalTrainer: Trainer | null
  ) {}
}

// export interface Client {
//   User: UserIn;
//   FirstName: string;
//   LastName: string;
//   Birthday: Date | null;
//   ImagePath: string;
//   Program: Program | null;
//   Goal: string;
//   InitialWeight: number;
//   CurrentWeight: number;
//   Height: number;
//   PersonalTrainer: Trainer | null;
// }
