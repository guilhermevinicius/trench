import { APP_INITIALIZER, ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { Environment, TrenchHttpRequestInterceptor } from './core/settings';
import { AuthenticationService } from './core/auth';

export function initializeApp(
  environment: Environment,
  authenticationService: AuthenticationService): () => Promise<void> {
  return async () => {
    await environment.load();
    await authenticationService.init();
    return await Promise.resolve();
  }
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptorsFromDi()),
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [Environment, AuthenticationService],
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TrenchHttpRequestInterceptor,
      multi: true
    }
  ]
};
