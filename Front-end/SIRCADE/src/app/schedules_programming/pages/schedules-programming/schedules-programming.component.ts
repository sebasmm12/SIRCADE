import { Component } from '@angular/core';
import { SchedulesProgrammingCalendarComponent } from '../schedules-programming-calendar/schedules-programming-calendar.component';

@Component({
  selector: 'app-schedules-programming',
  standalone: true,
  imports: [SchedulesProgrammingCalendarComponent],
  templateUrl: './schedules-programming.component.html',
  styleUrl: './schedules-programming.component.scss',
})
export class SchedulesProgrammingComponent {}
