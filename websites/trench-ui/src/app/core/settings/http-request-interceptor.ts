import { Router } from '@angular/router';
import { inject, Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';

import { from, Observable, switchMap } from 'rxjs';
import Keycloak from 'keycloak-js';

@Injectable({
  providedIn: 'root',
})
export class TrenchHttpRequestInterceptor implements HttpInterceptor {
  #keycloak = inject(Keycloak);
  #router = inject(Router)

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log("interceptor")

    if (this.#keycloak.authenticated) {
      console.log("ok");
    }

    return from("")
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
          this.#router.navigate(['signin'])
          return next.handle(req);
        }
      }));
  }
}
