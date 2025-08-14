import {Routes} from '@angular/router';
import {ProfileBaseComponent} from './views/profile-base/profile-base.component';

export const profileRoutes: Routes = [
  {
    path: ':username',
    component: ProfileBaseComponent
  }
]

export default profileRoutes;
