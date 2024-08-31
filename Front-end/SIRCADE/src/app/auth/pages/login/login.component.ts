import { Component, DestroyRef, inject } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { MaterialModule } from 'src/app/material.module';
import { CoreService } from 'src/app/services/core.service';
import { AccountsValidatorService } from '../../services/accounts-validator.service';
import { AccountsService } from '../../services/accounts.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, MaterialModule, FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  accountsValidatorService = inject(AccountsValidatorService);
  accountsService = inject(AccountsService);
  settingsService = inject(CoreService);

  router = inject(Router);
  formBuilder = inject(FormBuilder);
  destroyRef = inject(DestroyRef);
  generalErrorMessage = '';

  options = this.settingsService.getOptions();

  accountForm!: FormGroup;

  get f() {
    return this.accountForm.controls;
  }

  constructor() {
    this.buildForm();

    this.accountForm.valueChanges
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.generalErrorMessage = '';
      });
  }

  submit() {
    this.accountsService
      .login(this.accountForm.value)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (response) => {
          this.router.navigate(['/dashboards/dashboard1']);
        },
        error: (error: HttpErrorResponse) => {
          this.generalErrorMessage = error.error;
        },
      });
  }

  buildForm(): void {
    this.accountForm = this.formBuilder.group({
      nsa: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }
}
