import { BehaviorSubject, Observable, catchError, map, of, tap } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AUTH_PATH, PATH } from '../constants/uri-paths';
import { TokenStorageService } from './token-storage.service';
import { UserLoginResponseDTO } from '../models/user/user-login-response-dto';
import { TokensDTO } from '../models/token/tokens-dto';
import { UserLoginRequestDTO } from '../models/user/user-login-request-dto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public isAuthenticated = new BehaviorSubject<boolean>(false);
  public currentRole = new BehaviorSubject<string>('');
  public currentUser = new BehaviorSubject<UserLoginResponseDTO>(undefined);

  constructor(
    private http: HttpClient,
    private tokenStorageService: TokenStorageService
  ) {
    const token = this.tokenStorageService.getAccessToken();
    const refreshToken = this.tokenStorageService.getRefreshToken();
    const user = this.tokenStorageService.getUser();

    if (token && refreshToken && user) {
      this.isAuthenticated.next(true);
      this.currentRole.next(user.role);
      this.currentUser.next(user);
    }
  }

  login(email: string, password: string): Observable<boolean> {
    return this.http
      .post<TokensDTO>(
        PATH + AUTH_PATH + '/Login',
        new UserLoginRequestDTO(email, password)
      )
      .pipe(
        tap((tokens: TokensDTO) => {
          this.tokenStorageService.setAccessToken(tokens.accessToken);
          this.tokenStorageService.setRefreshToken(tokens.refreshToken);
          this.getUser().subscribe();
        }),
        map(() => true),
        catchError((error) => of(false)),
        tap((success: boolean) => this.isAuthenticated.next(success))
      );
  }

  getUser(): Observable<UserLoginResponseDTO> {
    return this.http.get<UserLoginResponseDTO>(PATH + AUTH_PATH + '/User').pipe(
      tap((user: UserLoginResponseDTO) => {
        if (!user.imageUrl) {
          user.imageUrl = '../../assets/ava.jpg';
        }
        this.tokenStorageService.setUser(user);
        this.currentRole.next(user.role);
        this.currentUser.next(user);
      }),
      map(() => this.tokenStorageService.getUser()),
      catchError((error) => {
        return of(undefined);
      })
    );
  }

  logOut() {
    return this.http.delete(PATH + AUTH_PATH + '/Logout').pipe(
      tap((response) => {
        this.tokenStorageService.signOut();
        this.currentRole.next('');
        this.currentUser.next(undefined);
        this.isAuthenticated.next(false);
      })
    );
  }

  refreshToken(refreshToken: string) {
    const params = new HttpParams().set('refreshToken', refreshToken);

    return this.http.post<TokensDTO>(PATH + AUTH_PATH + '/Refresh', null, {
      params,
    });
  }
}
