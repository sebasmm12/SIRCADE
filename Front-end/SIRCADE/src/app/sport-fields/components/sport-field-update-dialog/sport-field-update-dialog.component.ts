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
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { SportFieldsService } from '../../services/sport-fields.service';
import { SportFieldTypeService } from '../../services/sport-field-type.service';
import { SportFieldsValidatorService } from '../../services/sport-fields-validator.service';
import { SportFieldTypeResponse } from '../../interfaces/responses/sport-field-type.response';
import { forkJoin } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-sport-field-update-dialog',
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
  templateUrl: './sport-field-update-dialog.component.html',
  styleUrl: './sport-field-update-dialog.component.scss',
})
export class SportFieldUpdateDialogComponent {
  sportFieldId: number = inject(MAT_DIALOG_DATA);

  sportFieldsService = inject(SportFieldsService);
  sportFieldTypeService = inject(SportFieldTypeService);
  sportFieldsValidatorService = inject(SportFieldsValidatorService);

  destroyRef = inject(DestroyRef);
  formBuilder = inject(FormBuilder);

  sportFieldForm!: FormGroup;
  sportFieldTypes: SportFieldTypeResponse[] = [];

  loading: boolean = false;

  constructor(public dialogRef: MatDialogRef<SportFieldUpdateDialogComponent>) {
    this.buildForm();
  }

  ngOnInit(): void {
    this.loading = true;

    forkJoin({
      sportFieldTypes: this.sportFieldTypeService.getAll(),
      sportField: this.sportFieldsService.getById(this.sportFieldId),
    })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(({ sportFieldTypes, sportField }) => {
        this.sportFieldTypes = sportFieldTypes;
        this.sportFieldForm.setValue(sportField);
        this.loading = false;
      });
  }

  buildForm(): void {
    this.sportFieldForm = this.formBuilder.group({
      id: [0, [Validators.required]],
      name: ['', [Validators.required]],
      type: [0, [Validators.required]],
    });
  }

  update(): void {
    this.sportFieldForm.markAllAsTouched();
    this.sportFieldForm.updateValueAndValidity();

    if (this.sportFieldForm.invalid) {
      return;
    }

    this.sportFieldsService
      .update(this.sportFieldForm.value)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.dialogRef.close(true);
      });
  }
}
