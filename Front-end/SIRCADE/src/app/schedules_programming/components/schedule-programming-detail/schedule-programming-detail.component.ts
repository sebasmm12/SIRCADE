import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogRef,
} from '@angular/material/dialog';
import { ScheduleProgrammingInfoResponse } from '../../interfaces/responses/schedule-programming-info.response';
import { MaterialModule } from 'src/app/material.module';
import { CommonModule } from '@angular/common';
import { CoreService } from 'src/app/services/core.service';
import { ScheduleProgrammingCancelationConfirmationComponent } from '../schedule-programming-cancelation-confirmation/schedule-programming-cancelation-confirmation.component';
import { ResultMessageService } from '../../../shared/services/result-message.service';
import { ResultActionType } from 'src/app/shared/interfaces/enums/result-action-type';
import {
  MatTimepickerModule,
  provideNativeDateTimeAdapter,
} from '@dhutaryan/ngx-mat-timepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { SchedulesProgrammingValidatorService } from '../../services/schedules-programming-validator.service';
import { ScheduleProgrammingUpdateRequest } from '../../interfaces/requests/schedule-programming-update.request';
import { addHours, addMinutes, setHours } from 'date-fns';
import { SchedulesProgrammingService } from '../../services/schedules-programming.service';
import { HttpErrorResponse } from '@angular/common/http';
import { AccountsService } from 'src/app/auth/services/accounts.service';

@Component({
  selector: 'app-schedule-programming-detail',
  standalone: true,
  imports: [
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    MatTimepickerModule,
  ],
  templateUrl: './schedule-programming-detail.component.html',
  styleUrl: './schedule-programming-detail.component.scss',
  providers: [provideNativeDateAdapter(), provideNativeDateTimeAdapter()],
})
export class ScheduleProgrammingDetailComponent implements OnInit {
  coreService = inject(CoreService);
  dialogService = inject(MatDialog);
  resultMessageService = inject(ResultMessageService);
  scheduleProgrammingValidator = inject(SchedulesProgrammingValidatorService);
  scheduleProgrammingService = inject(SchedulesProgrammingService);
  accountsService = inject(AccountsService);

  destroyRef = inject(DestroyRef);
  formBuilder = inject(FormBuilder);

  scheduleProgrammingInfo: ScheduleProgrammingInfoResponse =
    inject(MAT_DIALOG_DATA);

  loading: boolean = false;
  scheduleProgrammingColor: string = '';
  currentDate: Date = new Date();
  startDate!: Date;
  scheduleProgrammingUpdateForm!: FormGroup;
  canEdit = false;
  generalErrorMessage = '';

  constructor(
    public dialogRef: MatDialogRef<ScheduleProgrammingDetailComponent>
  ) {}

  ngOnInit(): void {
    this.scheduleProgrammingColor =
      this.coreService.getOptions().theme === 'dark'
        ? this.scheduleProgrammingInfo.darkColor
        : this.scheduleProgrammingInfo.lightColor;

    this.currentDate.setHours(0, 0, 0, 0);
    this.startDate = new Date(this.scheduleProgrammingInfo.startDate);
    this.startDate.setHours(0, 0, 0, 0);

    this.buildForm();
  }

  close(): void {
    this.dialogRef.close();
  }

  reschedule(): void {
    this.canEdit = true;
  }

  showCancellationConfirmation(): void {
    this.dialogService
      .open(ScheduleProgrammingCancelationConfirmationComponent, {
        data: this.scheduleProgrammingInfo.id,
      })
      .afterClosed()
      .subscribe((result) => {
        if (!result) return;

        this.resultMessageService.showMessage(
          `La ${result} ha sido cancelada`,
          ResultActionType.Deletion
        );
        this.dialogRef.close(true);
      });
  }

  buildForm(): void {
    this.scheduleProgrammingUpdateForm = this.formBuilder.group(
      {
        id: [this.scheduleProgrammingInfo.id, Validators.required],
        type: [
          {
            id: this.scheduleProgrammingInfo.type,
            name: this.scheduleProgrammingInfo.typeName,
          },
        ],
        startDate: [this.startDate],
        startHour: [
          new Date(this.scheduleProgrammingInfo.startDate),
          [
            Validators.required,
            this.scheduleProgrammingValidator.validateHour(),
          ],
        ],
        endHour: [
          new Date(this.scheduleProgrammingInfo.endDate),
          [
            Validators.required,
            this.scheduleProgrammingValidator.validateHour(),
          ],
        ],
      },
      {
        validators: [this.scheduleProgrammingValidator.validateEndHour()],
      }
    );
  }

  todayDateFilter(date: Date | null): boolean {
    let today = new Date();
    today.setHours(0, 0, 0, 0);

    let dateToCompare = date || new Date();

    return (
      dateToCompare.getTime() >= today.getTime() &&
      dateToCompare.getFullYear() == today.getFullYear()
    );
  }

  cancelEdition(): void {
    this.canEdit = false;
  }

  save(): void {
    this.scheduleProgrammingUpdateForm.markAllAsTouched();
    this.scheduleProgrammingUpdateForm.updateValueAndValidity();

    if (this.scheduleProgrammingUpdateForm.invalid) return;

    const request: ScheduleProgrammingUpdateRequest = {
      id: this.scheduleProgrammingUpdateForm.get('id')?.value,
      startDate: this.getDate('startDate', 'startHour'),
      endDate: this.getDate('startDate', 'endHour'),
    };

    this.scheduleProgrammingService.reschedule(request).subscribe({
      next: () => {
        this.resultMessageService.showMessage(
          'La programación ha sido reprogramada',
          ResultActionType.Update
        );
        this.dialogRef.close(true);
      },
      error: (error: HttpErrorResponse) => {
        this.generalErrorMessage = error.error;
      },
    });

    // this.dialogRef.close(request);
  }

  getDate(startDateKey: string, hourKey: string): Date {
    let date: Date =
      this.scheduleProgrammingUpdateForm.get(startDateKey)?.value;

    date = setHours(date, -1 * date.getUTCHours());

    const hour: Date = this.scheduleProgrammingUpdateForm.get(hourKey)?.value;

    date = addHours(date, hour.getHours());
    date = addMinutes(date, hour.getMinutes());

    return date;
  }

  canShowActions(): boolean {
    const userRole = this.accountsService.getUser().role;
    return (
      this.startDate >= this.currentDate &&
      (userRole != 'Socio' ||
        this.accountsService.getUser().id ==
          this.scheduleProgrammingInfo.clientId)
    );
  }
}
