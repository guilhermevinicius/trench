import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserService } from './core/services/user.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  #userService = inject(UserService);

  ngOnInit(): void {
    this.#userService.me$()
  }
}
