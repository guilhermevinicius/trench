import {Routes} from '@angular/router';
import {SigninComponent} from './views/signin/signin.component';
import {SignupComponent} from './views/signup/signup.component';

export const mainRoute: Routes = [
  {
    path: 'signin',
    component: SigninComponent
  },
  {
    path: 'signup',
    component: SignupComponent
  }
];

export default mainRoute;
