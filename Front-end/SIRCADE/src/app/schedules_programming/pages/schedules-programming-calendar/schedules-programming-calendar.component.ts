import { CommonModule, NgSwitch } from '@angular/common';
import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
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
import { Title } from '@angular/platform-browser';
import { ScheduleProgrammingDetailComponent } from '../../components/schedule-programming-detail/schedule-programming-detail.component';
import { CoreService } from 'src/app/services/core.service';

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
  @Input('title')
  Title: string;

  @Input('register-title-button')
  RegisterButtonTitle: string;

  dialogService = inject(MatDialog);
  destroyRef = inject(DestroyRef);
  coreService = inject(CoreService);
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
        const color =
          this.coreService.getOptions().theme === 'dark'
            ? scheduleProgramming.darkColor
            : scheduleProgramming.lightColor;

        const scheduleProgrammingCssClass =
          scheduleProgramming.typeName == 'Mantenimiento'
            ? 'maintenance'
            : 'reservation';

        return {
          start: new Date(scheduleProgramming.startDate),
          end: new Date(scheduleProgramming.endDate),
          title: this.getTitleTemplate(scheduleProgramming),
          id: scheduleProgramming.id,
          meta: scheduleProgramming,
          color: {
            primary: color,
            secondary: color,
          },
          cssClass: scheduleProgrammingCssClass,
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

  showDetail(event: CalendarEvent) {
    this.dialogService
      .open(ScheduleProgrammingDetailComponent, {
        width: '600px',
        data: event.meta,
      })
      .afterClosed()
      .subscribe((result) => {
        if (!result) return;
        this.getAllByWeek();
      });
  }
}
