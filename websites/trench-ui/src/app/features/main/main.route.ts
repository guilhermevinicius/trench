import { Routes } from '@angular/router';
import MainLayoutComponent from './views/main-layout/main-layout.component';

export const mainRoute: Routes = [
  {
    path: 'p',
    component: MainLayoutComponent,
    loadChildren: () => import('../profile/profile.route').then(res => res.default),
  }
];

export default mainRoute;
