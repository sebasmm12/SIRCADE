import { Injectable } from '@angular/core';
import {
  AbstractControl,
  FormGroup,
  ValidationErrors,
  Validator,
  ValidatorFn,
} from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class SchedulesProgrammingValidatorService {
  constructor() {}

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
      case 'invalidHour':
        message = 'Hora inválida';
        break;
      case 'invalidEndHour':
        message = 'Hora fin tiene que ser mayor a la hora inicio';
        break;
      default:
        break;
    }

    return message;
  }

  validateHour(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value as Date;

      const hour = value?.getHours();
      const minutes = value?.getMinutes();

      if (hour < 6 || hour > 23) {
        return { invalidHour: true };
      }

      if (minutes != 0) {
        return { invalidHour: true };
      }

      return null;
    };
  }

  validateEndHour() {
    return (formGroup: AbstractControl): ValidationErrors | null => {
      const startHour = formGroup.get('startHour')?.value as Date;
      const endHour = formGroup.get('endHour')?.value as Date;

      if (endHour <= startHour) {
        formGroup.get('endHour')?.setErrors({ invalidEndHour: true });
      }

      return null;
    };
  }
}
