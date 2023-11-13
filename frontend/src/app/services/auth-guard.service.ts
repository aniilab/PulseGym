import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from './auth.service';

export const canActivateLoggedInGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const router = inject(Router);
  if (inject(AuthService).isAuthenticated()) {
    return true;
  } else {
    router.navigate(['/']);
    return false;
  }
};

export const canActivateIsAdminGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const router = inject(Router);
  if (inject(AuthService).getRole() === 'admin') {
    return true;
  } else {
    router.navigate(['/']);
    return false;
  }
};
