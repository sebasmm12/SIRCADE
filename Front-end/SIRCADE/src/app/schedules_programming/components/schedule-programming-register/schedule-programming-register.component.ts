import { CommonModule } from '@angular/common';
import {
  AfterViewInit,
  Component,
  DestroyRef,
  inject,
  OnInit,
  ViewChild,
} from '@angular/core';
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
  MAT_TIME_LOCALE,
  MatTimepicker,
  MatTimepickerModule,
  provideNativeDateTimeAdapter,
} from '@dhutaryan/ngx-mat-timepicker';
import { SportFieldInfoResponse } from 'src/app/sport-fields/interfaces/responses/sport-field-info.response';
import { SportFieldsService } from 'src/app/sport-fields/services/sport-fields.service';

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

  destroyRef = inject(DestroyRef);
  formBuilder = inject(FormBuilder);

  scheduleProgrammingForm!: FormGroup;
  sportFields: SportFieldInfoResponse[] = [];

  loading: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<ScheduleProgrammingRegisterComponent>
  ) {
    this.buildForm();
  }

  ngOnInit(): void {
    this.loading = true;

    this.sportFieldsService
      .getAll()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        this.sportFields = response;
        this.loading = false;
      });
  }

  buildForm(): void {
    this.scheduleProgrammingForm = this.formBuilder.group({
      sportFieldId: [null, Validators.required],
      startDate: [null, Validators.required],
      endDate: [null, Validators.required],
      comment: ['', [Validators.required]],
      updateProgrammingComment: ['', []],
    });
  }

  todayDateFilter(date: Date | null): boolean {
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    return (date || new Date()).getTime() >= today.getTime();
  }

  register(): void {}
}
