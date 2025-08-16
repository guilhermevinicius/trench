import { Router } from '@angular/router';
import { inject, Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../auth';

@Injectable({
  providedIn: 'root',
})
export class TrenchHttpRequestInterceptor implements HttpInterceptor {
  #authenticationService = inject(AuthenticationService);
  #router = inject(Router)

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.#authenticationService.getToken()

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
    }

    this.#router.navigate(['signin'])
    return next.handle(req);
  }
}
