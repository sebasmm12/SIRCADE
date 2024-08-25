import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { SportFieldsService } from '../../services/sport-fields.service';
import { SportFieldsValidatorService } from '../../services/sport-fields-validator.service';
import { SportFieldTypeService } from '../../services/sport-field-type.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { SportFieldTypeResponse } from '../../interfaces/responses/sport-field-type.response';

@Component({
  selector: 'app-sport-field-register-dialog',
  standalone: true,
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
  ],
  templateUrl: './sport-field-register-dialog.component.html',
  styleUrl: './sport-field-register-dialog.component.scss',
})
export class SportFieldRegisterDialogComponent {
  sportFieldsService = inject(SportFieldsService);
  sportFieldTypeService = inject(SportFieldTypeService);
  sportFieldsValidatorService = inject(SportFieldsValidatorService);

  destroyRef = inject(DestroyRef);
  formBuilder = inject(FormBuilder);

  sportFieldForm!: FormGroup;
  sportFieldTypes: SportFieldTypeResponse[] = [];

  loading: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<SportFieldRegisterDialogComponent>
  ) {
    this.buildForm();
  }

  ngOnInit(): void {
    this.loading = true;

    this.sportFieldTypeService
      .getAll()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((sportFieldTypes) => {
        this.sportFieldTypes = sportFieldTypes;
        this.loading = false;
      });
  }

  buildForm(): void {
    this.sportFieldForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      type: [null, [Validators.required]],
    });
  }

  register(): void {
    this.sportFieldForm.markAllAsTouched();
    this.sportFieldForm.updateValueAndValidity();

    if (this.sportFieldForm.invalid) {
      return;
    }

    this.sportFieldsService
      .register(this.sportFieldForm.value)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.dialogRef.close(true);
      });
  }
}
