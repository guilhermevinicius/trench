import { Component, inject } from '@angular/core';
import { provideIcons } from '@ng-icons/core';
import { IUsername } from 'app/core/models';
import { UserService } from 'app/core/services/user.service';
import * as hugeIcons from '@ng-icons/huge-icons'
import { ActivatedRoute, RouterModule } from '@angular/router';
import { EditProfileModalComponent } from "../../components";
import { FollowerService } from 'app/core/services/follower.service';

@Component({
  selector: 'app-profile-base',
  imports: [
    RouterModule,
    EditProfileModalComponent
  ],
  providers: [
    provideIcons({ ...hugeIcons })
  ],
  templateUrl: './profile-base.component.html',
  styleUrl: './profile-base.component.sass'
})
export class ProfileBaseComponent {
  #userService = inject(UserService);
  #followerService = inject(FollowerService);
  #router = inject(ActivatedRoute);
  user: IUsername | null = null;
  isOpenProfileModal = false;
  isNotFoundUser = false;
  username: string | null = null;

  constructor() {
    this.#router.params.subscribe({
      next: (e: any) => {
        this.username = e.username
        this.#loadUserByUsername()
      }
    })
  }

  sendRequestFollower(followingId: number) {
    this.#followerService.sendRequest$(followingId).subscribe({
      next: _ => this.#loadUserByUsername(),
      error: e => console.log(e)
    })
  }

  #loadUserByUsername() {
    if (this.username)
      this.#userService.username$(this.username).subscribe({
        next: res => this.user = res.data,
        error: e => this.isNotFoundUser = true
      })
  }

}
