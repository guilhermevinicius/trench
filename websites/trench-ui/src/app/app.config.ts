import {ApplicationConfig, provideZoneChangeDetection} from '@angular/core';
import {provideRouter} from '@angular/router';

import {routes} from './app.routes';
import {provideClientHydration, withEventReplay} from '@angular/platform-browser';
import {provideKeycloak} from 'keycloak-angular';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({eventCoalescing: true}),
    provideRouter(routes),
    provideClientHydration(withEventReplay()),
    provideKeycloak({
      config: {
        url: 'http://localhost:8080',
        realm: 'trench',
        clientId: 'trench-ui',
      },
      initOptions: {
        onLoad: 'check-sso',
        silentCheckSsoRedirectUri: typeof window !== 'undefined' ? `${window.location.origin}/silent-check-sso.html` : '',
      }
    }),
  ]
};
