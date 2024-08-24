import { Injectable } from '@angular/core';
import {
  AbstractControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class RolesValidatorService {
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
        message = 'This field is required';
        break;
      case 'maxlength':
        message = `${errors['maxlength'].actualLength}/${errors['maxlength'].requiredLength} Maximum characters exceeded`;
        break;
      case 'minlength':
        message = `${errors['minlength'].actualLength}/${errors['minlength'].requiredLength} Minimum characters required`;
        break;
      case 'pattern':
        message = 'Does not have a valid pattern';
        break;
      case 'invalidItem':
        message = 'Invalid string';
        break;
      case 'multipleErrors':
        message = 'Multiple errors exist';
        break;
      case 'validationError':
        message = errors['validationError'][0];
        break;
      case 'minimumElements':
        message = `At least ${errors['minimumElements'].minimumNumberOfElements} ${errors['minimumElements'].elementName} is required`;
        break;
      default:
        break;
    }

    return message;
  }

  validateMinimumElements(
    minimumNumberOfElements: number,
    elementName: string
  ): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value as any[];

      if (value.length < minimumNumberOfElements) {
        return { minimumElements: { elementName, minimumNumberOfElements } };
      }

      return null;
    };
  }
}
