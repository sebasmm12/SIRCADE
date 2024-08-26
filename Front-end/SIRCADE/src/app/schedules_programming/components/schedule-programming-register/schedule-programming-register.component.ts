import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatNativeDateModule,
  provideNativeDateAdapter,
} from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import {
  MatTimepickerModule,
  provideNativeDateTimeAdapter,
} from '@dhutaryan/ngx-mat-timepicker';
import { SportFieldInfoResponse } from 'src/app/sport-fields/interfaces/responses/sport-field-info.response';
import { SportFieldsService } from 'src/app/sport-fields/services/sport-fields.service';
import { SchedulesProgrammingValidatorService } from '../../services/schedules-programming-validator.service';
import { ProgrammingTypesService } from '../../services/programming-types.service';
import { forkJoin } from 'rxjs';
import { ProgrammingTypeInfoResponse } from '../../interfaces/responses/programming-type-info.response';

@Component({
  selector: 'app-schedule-programming-register',
  standalone: true,
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatInputModule,
    MatSelectModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatTimepickerModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
  ],
  templateUrl: './schedule-programming-register.component.html',
  styleUrl: './schedule-programming-register.component.scss',
  providers: [provideNativeDateAdapter(), provideNativeDateTimeAdapter()],
})
export class ScheduleProgrammingRegisterComponent implements OnInit {
  sportFieldsService = inject(SportFieldsService);
  programmingTypesService = inject(ProgrammingTypesService);
  scheduleProgrammingValidator = inject(SchedulesProgrammingValidatorService);

  destroyRef = inject(DestroyRef);
  formBuilder = inject(FormBuilder);

  scheduleProgrammingForm!: FormGroup;
  sportFields: SportFieldInfoResponse[] = [];
  programmingTypes: ProgrammingTypeInfoResponse[] = [];

  loading: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<ScheduleProgrammingRegisterComponent>
  ) {
    this.buildForm();
  }

  ngOnInit(): void {
    this.scheduleProgrammingForm.get('endHour')?.disable();

    this.loading = true;

    forkJoin({
      sportFields: this.sportFieldsService.getAll(),
      programmingTypes: this.programmingTypesService.getAll(),
    })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(({ sportFields, programmingTypes }) => {
        this.programmingTypes = programmingTypes;
        this.sportFields = sportFields;
        this.loading = false;
      });

    this.scheduleProgrammingForm
      .get('startHour')
      ?.valueChanges.pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        if (this.scheduleProgrammingForm.get('startHour')?.invalid) {
          this.scheduleProgrammingForm.get('endHour')?.disable();
          this.scheduleProgrammingForm.get('endHour')?.reset();
          return;
        }

        this.scheduleProgrammingForm.get('endHour')?.enable();
      });
  }

  buildForm(): void {
    this.scheduleProgrammingForm = this.formBuilder.group(
      {
        sportFieldId: [null, Validators.required],
        startDate: [null, Validators.required],
        startHour: [
          null,
          [
            Validators.required,
            this.scheduleProgrammingValidator.validateHour(),
          ],
        ],
        endHour: [
          null,
          [
            Validators.required,
            this.scheduleProgrammingValidator.validateHour(),
          ],
        ],
        clientId: [null],
        type: [null, Validators.required],
        comment: ['', [Validators.required]],
        updateProgrammingComment: ['', []],
      },
      {
        validators: [this.scheduleProgrammingValidator.validateEndHour()],
      }
    );
  }

  todayDateFilter(date: Date | null): boolean {
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    return (date || new Date()).getTime() >= today.getTime();
  }

  register(): void {
    console.log(this.scheduleProgrammingForm.value);
  }
}
