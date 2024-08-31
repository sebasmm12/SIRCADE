import { CommonModule, NgSwitch } from '@angular/common';
import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import {
  CalendarDateFormatter,
  CalendarEvent,
  CalendarModule,
} from 'angular-calendar';
import { endOfWeek, startOfDay, startOfWeek } from 'date-fns';
import { Subject } from 'rxjs';
import { ScheduleProgrammingRegisterComponent } from '../../components/schedule-programming-register/schedule-programming-register.component';
import { SchedulesProgrammingService } from '../../services/schedules-programming.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ScheduleProgrammingInfoResponse } from '../../interfaces/responses/schedule-programming-info.response';
import { SchedulesProgrammingWeeklyQueries } from '../../interfaces/queries/schedules-programming-weekly.queries';
import { getLocalDate } from 'src/app/shared/extensions/date.extensions';

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
export class SchedulesProgrammingCalendarComponent implements OnInit {
  dialogService = inject(MatDialog);
  destroyRef = inject(DestroyRef);
  scheduleProgrammingService = inject(SchedulesProgrammingService);

  view: any = 'month';
  calendarView: any = 'week';
  viewDate: Date = new Date();

  refresh: Subject<any> = new Subject();
  dayStartHour: number = 6;
  dayEndHour: number = 24;
  loading: boolean = false;
  schedulesProgramming: ScheduleProgrammingInfoResponse[] = [];
  events: CalendarEvent[] = [];

  constructor() {}

  showScheduleProgrammingRegister() {
    this.dialogService
      .open(ScheduleProgrammingRegisterComponent, {
        width: '800px',
      })
      .afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((registered) => {
        if (!registered) return;

        this.getAllByWeek();
      });
  }

  ngOnInit(): void {
    this.loading = true;

    this.getAllByWeek();
  }

  mapToCalendarEvents(
    schedulesProgramming: ScheduleProgrammingInfoResponse[]
  ): CalendarEvent[] {
    const events: CalendarEvent[] = schedulesProgramming.map(
      (scheduleProgramming) => {
        return {
          start: new Date(scheduleProgramming.startDate),
          end: new Date(scheduleProgramming.endDate),
          title: this.getTitleTemplate(scheduleProgramming),
        };
      }
    );

    return events;
  }

  getTitleTemplate(
    scheduleProgramming: ScheduleProgrammingInfoResponse
  ): string {
    let title = `<b>${scheduleProgramming.typeName}</b><br>
                <b>${scheduleProgramming.sportFieldName}</b><br>`;

    if (scheduleProgramming.clientId) {
      title += `<b>Socio: ${scheduleProgramming.clientName}</b><br>`;
    }

    return title;
  }

  goToNextWeek(): void {
    this.getAllByWeek();
  }

  goToPreviousWeek(): void {
    this.getAllByWeek();
  }

  goToToday(): void {
    this.getAllByWeek();
  }

  getAllByWeek(): void {
    const schedulesProgrammingQueries: SchedulesProgrammingWeeklyQueries = {
      startDate: getLocalDate(startOfWeek(this.viewDate)).toISOString(),
      endDate: getLocalDate(endOfWeek(this.viewDate)).toISOString(),
    };

    this.scheduleProgrammingService
      .getAllByWeek(schedulesProgrammingQueries)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        this.schedulesProgramming = response;
        this.events = this.mapToCalendarEvents(this.schedulesProgramming);
        this.loading = false;
      });
  }
}
