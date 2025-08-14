import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate } from "@angular/router";
import {Observable, of} from "rxjs";

@Injectable({ providedIn: 'root' })
export class TrenchAuthorizationGuard implements CanActivate {
  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {
    return of(true);
  }
}
