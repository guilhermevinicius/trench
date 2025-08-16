import { Injectable } from '@angular/core';

// export function InitEnvironment(environment: Environment) {
//   return async () => await environment.load();
// }

@Injectable({
  providedIn: 'root'
})
export class Environment {
  appname = '';
  environment = '';
  isProduction = false;
  sessionChecks = false;

  apiBff = '';

  auth = {
    url: '',
    realm: '',
    clientId: ''
  }

  async load() {
    const environmentFileName = 'environment.json';
    try {
      const response = await fetch(environmentFileName);
      const values: Environment = await response.json();

      this.appname = values.appname;
      this.environment = values.environment;
      this.isProduction = values.isProduction;
      this.sessionChecks = values.sessionChecks;
      this.auth = {
        url: values.auth.url,
        realm: values.auth.realm,
        clientId: values.auth.clientId
      };
      this.apiBff = values.apiBff;
    } catch (e) {
      console.log(`Could not load "${environmentFileName}"`);
    }
  }
}
