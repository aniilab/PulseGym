import {
  HTTP_INTERCEPTORS,
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TokenStorageService } from '../services/token-storage.service';
import {
  BehaviorSubject,
  Observable,
  catchError,
  switchMap,
} from 'rxjs';
import { AUTH_PATH } from '../constants/uri-paths';
import { AuthService } from '../services/auth.service';
import { TokensDTO } from '../models/token/tokens-dto';

const ACCESS_TOKEN_HEADER_KEY = 'Authorization';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {
  private isRefreshing = false;

  constructor(
    private tokenStorageService: TokenStorageService,
    private authService: AuthService
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let authReq = req;
    const token = this.tokenStorageService.getAccessToken();

    if (token) {
      authReq = this.addTokenHeader(req, token);
    }

    return next.handle(authReq).pipe(
      catchError((error) => {
        if (
          error instanceof HttpErrorResponse &&
          !authReq.url.includes(AUTH_PATH + '/Login') &&
          error.status === 401
        ) {
          this.handle401Error(authReq, next).subscribe();
        }

        throw error;
      })
    );
  }

  private addTokenHeader(
    request: HttpRequest<any>,
    token: string
  ): HttpRequest<any> {
    return request.clone({
      headers: request.headers.set(ACCESS_TOKEN_HEADER_KEY, 'bearer ' + token),
    });
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;

      const refreshToken = this.tokenStorageService.getRefreshToken();

      if (refreshToken) {
        return this.authService.refreshToken(refreshToken).pipe(
          switchMap((tokens: TokensDTO) => {
            this.isRefreshing = false;

            this.tokenStorageService.setAccessToken(tokens.accessToken);
            this.tokenStorageService.setRefreshToken(tokens.refreshToken);

            return next.handle(
              this.addTokenHeader(request, tokens.accessToken)
            );
          }),
          catchError((error) => {
            this.isRefreshing = false;
            this.tokenStorageService.signOut();
            throw error;
          })
        );
      } else {
        this.isRefreshing = false;
        this.tokenStorageService.signOut();
        throw Error('No refresh token available');
      }
    } else {
      return new Observable<HttpEvent<any>>;
    }
  }
}

export const authInterceptorProviders = [
  {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthorizationInterceptor,
    multi: true,
  },
];
