import { Routes } from '@angular/router';
import { FeedComponent } from './views/feed/feed.component';

export const homeRoutes: Routes = [
  {
    path: '',
    component: FeedComponent
  }
]

export default homeRoutes;
