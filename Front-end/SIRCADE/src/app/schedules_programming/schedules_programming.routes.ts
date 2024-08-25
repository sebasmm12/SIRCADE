import { Routes } from '@angular/router';
import { SchedulesProgrammingCalendarComponent } from './pages/schedules-programming-calendar/schedules-programming-calendar.component';

export const schedulesProgrammingRoutes: Routes = [
  {
    path: '',
    component: SchedulesProgrammingCalendarComponent,
    data: {
      title: 'Programación de horarios',
      urls: [
        { title: 'Dashboard', url: '/dashboards/dashboard1' },
        { title: 'Programación de horarios' },
      ],
    },
  },
];
