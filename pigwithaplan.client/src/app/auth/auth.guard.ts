import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { map, take } from 'rxjs';

export const AuthGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.checkToken().pipe(
    take(1),
    map((isAuthenticated) => {
      if (!isAuthenticated) {
        router.navigate(['/login']);
        return false;
      }

      return true;
    })
  );
};
