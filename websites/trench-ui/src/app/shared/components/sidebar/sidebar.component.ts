import { Component, inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { IUser } from 'app/core/models';
import { UserService } from 'app/core/services/user.service';
import * as hugeIcons from '@ng-icons/huge-icons'

@Component({
  selector: 'app-sidebar',
  imports: [RouterModule, NgIcon],
  providers: [
    provideIcons({ ...hugeIcons })
  ],
  templateUrl: './sidebar.component.html'
})
export class SidebarComponent implements OnInit {
  #userService = inject(UserService);

  user: IUser | null = null;

  MENUS = [
    {
      label: 'Home',
      icon: 'hugeHome02',
      to: ''
    },
    {
      label: 'Trends',
      icon: 'hugeThreads',
      to: ''
    },
    {
      label: 'Create Post',
      icon: 'hugePlusSign',
      to: ''
    },
    {
      label: 'Reels',
      icon: 'hugePlayList',
      to: ''
    },
    {
      label: 'activity',
      icon: 'hugeHealth',
      to: 'activity'
    }
  ]

  ngOnInit(): void {
    this.#userService.user$.subscribe({
      next: user => this.user = user
    })
  }
}
