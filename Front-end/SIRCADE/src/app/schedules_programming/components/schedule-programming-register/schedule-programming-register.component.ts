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
import { ScheduleProgrammingRequest } from '../../interfaces/requests/schedule-programming.request';
import { addHours, addMinutes, setDate, setHours } from 'date-fns';
import { SchedulesProgrammingService } from '../../services/schedules-programming.service';
import { AccountsService } from 'src/app/auth/services/accounts.service';
import { HttpErrorResponse } from '@angular/common/http';

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
  schedulesProgrammingService = inject(SchedulesProgrammingService);
  accountsService = inject(AccountsService);

  scheduleProgrammingValidator = inject(SchedulesProgrammingValidatorService);

  destroyRef = inject(DestroyRef);
  formBuilder = inject(FormBuilder);

  scheduleProgrammingForm!: FormGroup;
  sportFields: SportFieldInfoResponse[] = [];
  programmingTypes: ProgrammingTypeInfoResponse[] = [];

  loading: boolean = false;
  generalErrorMessage = '';

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

        this.disableType();

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
    this.scheduleProgrammingForm.markAllAsTouched();
    this.scheduleProgrammingForm.updateValueAndValidity();

    if (this.scheduleProgrammingForm.invalid) return;

    let request: ScheduleProgrammingRequest = {
      sportFieldId: this.scheduleProgrammingForm.get('sportFieldId')?.value,
      clientId: this.scheduleProgrammingForm.get('clientId')?.value,
      comment: this.scheduleProgrammingForm.get('comment')?.value,
      type: this.scheduleProgrammingForm.get('type')?.value.id,
      startDate: this.getDate('startDate', 'startHour'),
      endDate: this.getDate('startDate', 'endHour'),
    };

    this.schedulesProgrammingService
      .register(request)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.dialogRef.close(true);
        },
        error: (error: HttpErrorResponse) => {
          this.generalErrorMessage = error.error;
        },
      });
  }

  getDate(startDateKey: string, hourKey: string): Date {
    let date: Date = this.scheduleProgrammingForm.get(startDateKey)?.value;

    date = setHours(date, -1 * date.getUTCHours());

    const hour: Date = this.scheduleProgrammingForm.get(hourKey)?.value;

    date = addHours(date, hour.getHours());
    date = addMinutes(date, hour.getMinutes());

    return date;
  }

  disableType(): void {
    if (this.accountsService.User?.role != 'Socio') return;

    this.scheduleProgrammingForm
      .get('clientId')
      ?.setValue(this.accountsService.User?.id);

    this.scheduleProgrammingForm.get('type')?.disable();

    const type = this.programmingTypes.find(
      (programmingType) => programmingType.name == 'Reserva'
    );

    this.scheduleProgrammingForm.get('type')?.setValue(type);
  }
}
