import { Router } from '@angular/router';
import { inject, Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';

import { from, Observable, switchMap } from 'rxjs';
import { AuthenticationService } from '../auth';

@Injectable({
  providedIn: 'root',
})
export class AcsHttpRequestInterceptor implements HttpInterceptor {
  #authenticationService = inject(AuthenticationService);

  constructor(
    private router: Router
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(this.#authenticationService.getValidToken())
      .pipe(switchMap((token) => {
        if (token) {
          const request = req.clone({
            setHeaders: {
              Authorization: `Bearer ${token}`,
              'Accept-Language': 'pt-BR',
              'Cache-Control': 'no-cache, no-store, must-revalidate, post-check=0, pre-check=0',
              'Pragma': 'no-cache',
              'Expires': '0',
            },
          });
          return next.handle(request);
        } else {
          this.router.navigate(['signin'])
          return next.handle(req);
        }
      }));
  }
}
