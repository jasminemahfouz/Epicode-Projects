import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { map, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authSvc: AuthService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    if (this.authSvc.syncIsLoggedIn) {
      return true;
    } else {
      return this.authSvc.isLoggedIn$.pipe(
        take(1),
        map(isLoggedIn => {
          if (isLoggedIn) {
            return true;
          } else {
            this.router.navigate(['/auth/login']);
            return false;
          }
        })
      );
    }
  }

  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    return this.canActivate(childRoute, state);
  }
}
