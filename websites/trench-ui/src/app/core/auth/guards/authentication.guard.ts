import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';

import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AcsAuthenticationGuard implements CanActivate {
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) { }

  async canActivate(_next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const user = localStorage.getItem('access-token');

    if (!user) {
      if (!state.url.startsWith('signin')) {
        // this.authenticationService.returnUrl = state.url;
      } else {
        // this.authenticationService.returnUrl = '/';
      }

      this.router.navigate(['signin']);

      return false;
    }

    return true;
  }
}
