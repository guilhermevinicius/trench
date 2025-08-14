import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Observable } from 'rxjs';

export const countingAuthorizationGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  //const countingUserAccountService = inject(CountingUserAccountService);
  let isAllowed: boolean | Observable<boolean> = false;

//   if (route?.routeConfig?.data && route.routeConfig.data.permissions && route.routeConfig.data.permissions.length > 0) {
//     isAllowed = countingUserAccountService.isClaimAllowed(route.routeConfig.data.permissions);
//   } else {
//     throw Error('When using authorization guard the route most have data.permissions[] configured');
//   }

//   if (isObservable(isAllowed)) {
//     isAllowed.subscribe({
//       next: (result) => {
//         if (!result) {
//           router.navigate([COLPORTEUR_CONSTANTS.UNAUTHORIZED_PATH]);
//         }
//       },
//       error: (e) => console.log(e)
//     });
//   } else {
//     if (!isAllowed) {
//       router.navigate([COLPORTEUR_CONSTANTS.UNAUTHORIZED_PATH]);
//     }
//   }

  return isAllowed;
}
