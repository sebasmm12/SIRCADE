import { Routes } from '@angular/router';
import { FrequentlyUsersReportComponent } from './pages/frequently-users-report/frequently-users-report.component';
import { CancelledReservationsByUserReportComponent } from './pages/cancelled-reservations-by-user-report/cancelled-reservations-by-user-report.component';
import { MonthlyReservationsComponent } from './pages/monthly-reservations/monthly-reservations.component';
import { YearlyReservationsComponent } from './pages/yearly-reservations/yearly-reservations.component';
import { DailyReservationsComponent } from './pages/daily-reservations/daily-reservations.component';
import { WeeklyReservationsComponent } from './pages/weekly-reservations/weekly-reservations.component';
import { SportFieldTypesByTurnComponent } from './pages/sport-field-types-by-turn/sport-field-types-by-turn.component';

export const routes: Routes = [
  {
    path: 'socios-frecuentes',
    component: FrequentlyUsersReportComponent,
    data: {
      title: 'Socios Frecuentes',
      urls: [
        { title: 'Dashboard', url: '/principal' },
        { title: 'Socios Frecuentes' },
      ],
    },
  },
  {
    path: 'cancelaciones-socio',
    component: CancelledReservationsByUserReportComponent,
    data: {
      title: 'Cancelaciones por Socios',
      urls: [
        { title: 'Dashboard', url: '/principal' },
        { title: 'Cancelaciones por Socios' },
      ],
    },
  },
  {
    path: 'reservas-mensuales',
    component: MonthlyReservationsComponent,
    data: {
      title: 'Reservas Mensuales',
      urls: [
        { title: 'Dashboard', url: '/principal' },
        { title: 'Reservas Mensuales' },
      ],
    },
  },
  {
    path: 'reservas-anuales',
    component: YearlyReservationsComponent,
    data: {
      title: 'Reservas Anuales',
      urls: [
        { title: 'Dashboard', url: '/principal' },
        { title: 'Reservas Anuales' },
      ],
    },
  },
  {
    path: 'reservas-diarias',
    component: DailyReservationsComponent,
    data: {
      title: 'Reservas Diarias',
      urls: [
        { title: 'Dashboard', url: '/principal' },
        { title: 'Reservas Diarias' },
      ],
    },
  },
  {
    path: 'reservas-semanales',
    component: WeeklyReservationsComponent,
    data: {
      title: 'Reservas Semanales',
      urls: [
        { title: 'Dashboard', url: '/principal' },
        { title: 'Reservas Semanales' },
      ],
    },
  },
  {
    path: 'canchas-por-turno',
    component: SportFieldTypesByTurnComponent,
    data: {
      title: 'Canchas deportiva por Turno',
      urls: [
        { title: 'Dashboard', url: '/principal' },
        { title: 'Canchas deportiva por Turno' },
      ],
    },
  },
];
