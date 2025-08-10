import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./features/main/main.route')
  },
  {
    path: '',
    loadChildren: () => import('./features/identity/identity.route'),

  }
];
