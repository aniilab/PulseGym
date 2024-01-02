import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from '../services/auth.service';



export const canActivateLoggedInGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const router = inject(Router);
  const isLoggedIn = inject(AuthService).isAuthenticated.getValue(); 
     if (isLoggedIn) {
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
  const role = inject(AuthService).currentRole.getValue();
    if (role === 'Admin') {
      return true;
    } else {
      router.navigate(['/']);
      return false;
    }
};
