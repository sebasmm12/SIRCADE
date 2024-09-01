import { Component } from '@angular/core';
import { SchedulesProgrammingCalendarComponent } from '../schedules-programming-calendar/schedules-programming-calendar.component';

@Component({
  selector: 'app-reservations',
  standalone: true,
  imports: [SchedulesProgrammingCalendarComponent],
  templateUrl: './reservations.component.html',
  styleUrl: './reservations.component.scss',
})
export class ReservationsComponent {}
