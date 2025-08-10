import { Injectable } from '@angular/core';
import { IAuthenticationModel } from '../models/i-authentication.model';

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

  authentication = { issuer: '', clientId: '', scope: '' } as IAuthenticationModel;

  firebase = {
    apiKey: '',
    authDomain: '',
    projectId: '',
    appId: '',
    messagingSenderId: '',
    useFireBase: true
  };

  authIatecSetting = {
    authUrl: '',
    appUrl: '',
    containerId: ''
  };

  async load() {
    const environmentFileName = 'environment.json';
    try {
      const response = await fetch(environmentFileName);
      const values: Environment = await response.json();

      this.appname = values.appname;
      this.environment = values.environment;
      this.isProduction = values.isProduction;
      this.sessionChecks = values.sessionChecks;

      this.apiBff = values.apiBff;

      this.authentication = values.authentication;

      this.firebase = values.firebase

      this.authIatecSetting = values.authIatecSetting
    } catch (e) {
      console.log(`Could not load "${environmentFileName}"`);
    }
  }
}
