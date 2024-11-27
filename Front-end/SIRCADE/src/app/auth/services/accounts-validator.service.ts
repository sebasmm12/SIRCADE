import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup, ValidationErrors } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class AccountsValidatorService {
  constructor() { }

  isFieldInvalid(
    formControl: FormGroup,
    fieldName: string
  ): boolean | undefined {
    return (
      formControl.get(fieldName)?.invalid && formControl.get(fieldName)?.touched
    );
  }

  showErrorMessage(formGroup: FormGroup, fieldName: string): string {
    let errors = formGroup.controls[fieldName].errors;
    let message = '';

    if (!errors) return message;

    let firstError = Object.keys(errors)[0];

    switch (firstError) {
      case 'required':
        message = 'El campo es requerido';
        break;
      case 'maxlength':
        message = `${errors['maxlength'].actualLength}/${errors['maxlength'].requiredLength} máximo caracteres excedido`;
        break;
      case 'minlength':
        message = `${errors['minlength'].actualLength}/${errors['minlength'].requiredLength} mínimo caracteres excedido`;
        break;
      case 'pattern':
        message = 'No tiene un formato válido';
        break;
      case 'invalidItem':
        message = 'Texto inválido';
        break;
      case 'multipleErrors':
        message = 'Múltiples errores';
        break;
      case 'validationError':
        message = errors['validationError'][0];
        break;
      default:
        break;
    }

    return message;
  }

  validateNewPasswordConfirmation() {
    return (formGroup: AbstractControl): ValidationErrors | null => {
      const password = formGroup.get('newPassword')?.value;
      const passwordConfirmation = formGroup.get('newPasswordConfirmation')?.value;

      if (password != passwordConfirmation) {
        formGroup.get('newPasswordConfirmation')?.setErrors({ validationError: ['Las contraseñas no coinciden'] });
      }

      return null;
    };
  }
}
