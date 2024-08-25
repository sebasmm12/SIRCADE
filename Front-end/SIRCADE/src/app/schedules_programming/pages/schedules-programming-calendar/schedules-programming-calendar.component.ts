import { CommonModule, NgSwitch } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CalendarDateFormatter, CalendarModule } from 'angular-calendar';
import { startOfDay } from 'date-fns';
import { vi } from 'date-fns/locale';
import { Subject } from 'rxjs';
import { MaterialModule } from 'src/app/material.module';
import { ScheduleProgrammingRegisterComponent } from '../../components/schedule-programming-register/schedule-programming-register.component';

@Component({
  selector: 'app-schedules-programming-calendar',
  standalone: true,
  imports: [
    MatDatepickerModule,
    MatDialogModule,
    MatFormFieldModule,
    MatCardModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    NgSwitch,
    CalendarModule,
    CommonModule,
  ],
  templateUrl: './schedules-programming-calendar.component.html',
  styleUrl: './schedules-programming-calendar.component.scss',
  providers: [provideNativeDateAdapter(), CalendarDateFormatter],
})
export class SchedulesProgrammingCalendarComponent {
  view: any = 'month';
  calendarView: any = 'week';
  viewDate: Date = new Date();

  refresh: Subject<any> = new Subject();
  dayStartHour: number = 6;
  dayEndHour: number = 24;

  dialogService = inject(MatDialog);

  constructor() {}

  showScheduleProgrammingRegister() {
    this.dialogService.open(ScheduleProgrammingRegisterComponent, {
      width: '800px',
    });
  }
}
