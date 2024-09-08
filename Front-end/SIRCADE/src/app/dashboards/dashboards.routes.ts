import { Routes } from '@angular/router';
import { MainDashboardComponent } from './pages/main-dashboard/main-dashboard.component';

export const dashboardRoutes: Routes = [
  {
    path: '',
    component: MainDashboardComponent,
    data: {
      title: 'Menú principal',
      urls: [
        { title: 'Dashboard', url: '/principal' },
        { title: 'Menú principal' },
      ],
    },
  },
];
