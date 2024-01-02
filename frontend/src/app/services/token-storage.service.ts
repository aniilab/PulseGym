import { Injectable } from '@angular/core';
import { UserLoginResponseDTO } from '../models/user/user-login-response-dto';

const ACCESSTOKEN_KEY = 'auth-token';
const REFRESHTOKEN_KEY = 'auth-refreshtoken';
const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {
  constructor() { }

  signOut(): void {
    window.localStorage.clear();
  }

  public setAccessToken(accessToken: string): void {
    window.localStorage.removeItem(ACCESSTOKEN_KEY);
    window.localStorage.setItem(ACCESSTOKEN_KEY, accessToken);
  }

  public getAccessToken(): string | null {
    return window.localStorage.getItem(ACCESSTOKEN_KEY);
  }

  public setRefreshToken(token: string): void {
    window.localStorage.removeItem(REFRESHTOKEN_KEY);
    window.localStorage.setItem(REFRESHTOKEN_KEY, token);
  }

  public getRefreshToken(): string | null {
    return window.localStorage.getItem(REFRESHTOKEN_KEY);
  }

  public setUser(user: UserLoginResponseDTO): void {
    window.localStorage.removeItem(USER_KEY);
    window.localStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public getUser(): any {
    const user = window.localStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }
}