import { Routes } from '@angular/router';
import { RolesListComponent } from './pages/roles-list/roles-list.component';

export const roleRoutes: Routes = [
  {
    path: '',
    component: RolesListComponent,
    data: {
      title: 'Roles',
      urls: [
        { title: 'Dashboard', url: '/dashboards/dashboard1' },
        { title: 'Roles' },
      ],
    },
  },
];
