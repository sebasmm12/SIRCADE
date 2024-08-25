import { Routes } from '@angular/router';
import { SportFieldsListComponent } from './pages/sport-fields-list/sport-fields-list.component';

export const sportFieldsRoutes: Routes = [
  {
    path: '',
    component: SportFieldsListComponent,
    data: {
      title: 'Canchas Deportivas',
      urls: [
        { title: 'Dashboard', url: '/dashboards/dashboard1' },
        { title: 'Canchas Deportivas' },
      ],
    },
  },
];
