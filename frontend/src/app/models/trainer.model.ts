import { UserRequest } from "./userRequest.model";

export class Trainer {
  constructor(
    public User: UserRequest,
    public FirstName: string,
    public LastName: string,
    public PicturePath: string,
    public Category: string,
    public Birthday: Date | null
  ) {}
}

// export interface Trainer {
//   UserId: number;
//   FirstName: string;
//   LastName: string;
//   PicturePath: string;
//   Category: string;
//   Birthday: Date | null;
// }
