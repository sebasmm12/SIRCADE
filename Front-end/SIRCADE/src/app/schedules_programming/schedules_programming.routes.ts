import { Routes } from '@angular/router';
import { SchedulesProgrammingCalendarComponent } from './pages/schedules-programming-calendar/schedules-programming-calendar.component';
import { ReservationsComponent } from './pages/reservations/reservations.component';
import { SchedulesProgrammingComponent } from './pages/schedules-programming/schedules-programming.component';

export const schedulesProgrammingRoutes: Routes = [
  {
    path: 'programacion-horarios',
    component: SchedulesProgrammingComponent,
    data: {
      title: 'Programación de horarios',
      urls: [
        { title: 'Dashboard', url: '/dashboards/dashboard1' },
        { title: 'Programación de horarios' },
      ],
    },
  },
  {
    path: 'reservas',
    component: ReservationsComponent,
    data: {
      title: 'Reservas',
      urls: [
        { title: 'Dashboard', url: '/dashboards/dashboard1' },
        { title: 'Reservas' },
      ],
    },
  },
];
