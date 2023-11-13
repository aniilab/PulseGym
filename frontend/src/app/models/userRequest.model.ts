export class UserRequest {
  constructor(
    public Id: number,
    public LoginName: string,
    public Password: string,
    public Role: string
  ) {}
}

// export interface UserRequest {
//   Id: number;
//   LoginCode: string;
//   Password: string;
//   Role: string;
// }
