import { Subject } from 'rxjs';
import { UserRequest } from '../models/userRequest.model';

export class AuthService {
  public authChanged = new Subject<boolean>();
  public authRoleChanged = new Subject<string>();

  private isLoggedIn = false;
  private role: string = '';
  private users: UserRequest[] = [
    new UserRequest(0, 'admin@gmail.com', 'admin123', 'admin'),
    new UserRequest(1, 'trainer@gmail.com', 'trainer123', 'trainer'),
    new UserRequest(2, 'client@gmail.com', 'client123', 'client'),
  ];

  isAuthenticated(): boolean {
    return this.isLoggedIn;
  }

  getRole() {
    return this.role;
  }

  getUser(id: number) {
    return this.users.at(id);
  }

  logIn(email: string, password: string): boolean {
    for (let user of this.users) {
      if (user.LoginName === email && user.Password === password) {
        this.role = user.Role;
        this.isLoggedIn = true;
        this.authChanged.next(this.isLoggedIn);
        this.authRoleChanged.next(this.role);
        return true;
      }
    }
    return false;
  }

  logOut() {
    this.role = '';
    this.isLoggedIn = false;
    this.authChanged.next(this.isLoggedIn);
    this.authRoleChanged.next(this.role);
  }
}
