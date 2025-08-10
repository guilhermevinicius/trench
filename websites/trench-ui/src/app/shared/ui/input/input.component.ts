import {Component, forwardRef, Injector, Input} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl, ReactiveFormsModule} from '@angular/forms';

@Component({
  selector: 'app-input',
  imports: [
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputComponent),
      multi: true,
    }
  ],
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss'
})
export class InputComponent implements ControlValueAccessor {
  isFocused = false;
  value: string = '';
  disabled: boolean = false;
  ngControl: NgControl | null = null;

  @Input({required: true}) label: string = '';
  @Input() type: 'text' | 'password' | 'email' = 'text';
  @Input() icon: string | null = null;

  constructor(private injector: Injector) {
    setTimeout(() => {
      this.ngControl = this.injector.get(NgControl, null);
    }, 0);
  }

  onInput(event: Event): void {
    const inputValue = (event.target as HTMLInputElement).value;
    this.value = inputValue;
    this.onChange(inputValue);
  }

  writeValue(value: string): void {
    this.value = value || '';
  }

  registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  getErrorMessage(): string {
    if (!this.ngControl?.errors) {
      return '';
    }

    const errors = this.ngControl.errors;
    if (errors['required']) {
      return 'Este campo é obrigatório';
    } else if (errors['email']) {
      return 'Digite um e-mail válido';
    } else if (errors['minlength']) {
      return `O campo deve ter pelo menos ${errors['minlength'].requiredLength} caracteres`;
    }
    return 'Campo inválido';
  }

  private onChange: (value: string) => void = () => {
  };

  onTouched: () => void = () => {
    this.isFocused = true;
  };
}
