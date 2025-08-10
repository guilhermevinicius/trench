import { Component } from '@angular/core';
import {ButtonComponent} from '@shared/ui/button/button.component';
import {InputComponent} from '@shared/ui/input/input.component';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';

@Component({
  selector: 'app-signin',
  imports: [
    ButtonComponent,
    InputComponent,
    ReactiveFormsModule
  ],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.scss'
})
export class SigninComponent {
  loading = false;

  signInForm = new FormGroup({
    email: new FormControl<string | null>(null, [Validators.required, Validators.email]),
    password: new FormControl<string | null>(null, [Validators.required, Validators.minLength(6)]),
  })

  signInSubmit() {
    console.log("SignIn")
    this.loading = true;
  }
}
