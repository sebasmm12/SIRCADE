import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MaterialModule } from 'src/app/material.module';
import { AccountsValidatorService } from '../../services/accounts-validator.service';
import { AccountsService } from '../../services/accounts.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ResultMessageService } from 'src/app/shared/services/result-message.service';
import { ResultActionType } from 'src/app/shared/interfaces/enums/result-action-type';
import { Router } from '@angular/router';

@Component({
  selector: 'app-password-update',
  standalone: true,
  imports: [MaterialModule, FormsModule, ReactiveFormsModule],
  templateUrl: './password-update.component.html',
  styleUrl: './password-update.component.scss',
})
export class PasswordUpdateComponent implements OnInit {
  formBuilder = inject(FormBuilder);
  accountsValidatorService = inject(AccountsValidatorService);
  accountsService = inject(AccountsService);
  destroyRef = inject(DestroyRef);
  resultMessageService = inject(ResultMessageService);
  router = inject(Router);

  isCurrentPasswordHidden: boolean = true;
  isNewPasswordHidden: boolean = true;
  isNewPasswordConfirmationHidden: boolean = true;
  generalErrorMessage: string = '';

  passwordUpdateForm: FormGroup;

  changeCurrentPasswordVisibility() {
    this.isCurrentPasswordHidden = !this.isCurrentPasswordHidden;
  }

  changeNewPasswordVisibility() {
    this.isNewPasswordHidden = !this.isNewPasswordHidden;
  }

  changeNewPasswordConfirmationVisibility() {
    this.isNewPasswordConfirmationHidden =
      !this.isNewPasswordConfirmationHidden;
  }

  ngOnInit(): void {
    this.buildForm();
  }

  buildForm(): void {
    this.passwordUpdateForm = this.formBuilder.group(
      {
        currentPassword: ['', [Validators.required, Validators.minLength(8)]],
        newPassword: ['', [Validators.required, Validators.minLength(8)]],
        newPasswordConfirmation: [
          '',
          [Validators.required, Validators.minLength(8)],
        ],
      },
      {
        validators:
          this.accountsValidatorService.validateNewPasswordConfirmation(),
      }
    );
  }

  changePassword(): void {
    this.passwordUpdateForm.markAllAsTouched();

    if (this.passwordUpdateForm.invalid) return;

    this.accountsService
      .updatePassword(this.passwordUpdateForm.value)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(
        {
          next: () => {
            this.resultMessageService.showMessage(
              'Contraseña actualizada correctamente',
              ResultActionType.Update);
            this.router.navigate(['/principal']);
          },
          error: (error) => {
            this.generalErrorMessage = error.error;
          },
        }
      );
  }
}
