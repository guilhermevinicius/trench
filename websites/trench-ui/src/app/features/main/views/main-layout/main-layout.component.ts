import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from '@shared/components';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    SidebarComponent
  ],
  templateUrl: './main-layout.component.html',
})
export default class MainLayoutComponent {}
