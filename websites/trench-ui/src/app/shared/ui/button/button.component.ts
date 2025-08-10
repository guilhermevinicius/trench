import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-button',
  imports: [],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent {
  @Input({required: true}) label: string = '';
  @Input() type: 'submit' | 'button' = 'button';
  @Input() disabled: boolean = false;
  @Input() loading: boolean = false;
}
