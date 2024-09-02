import { Routes } from '@angular/router';
import { WorkersListComponent } from './pages/workers-list/workers-list.component';
import { ClientsListComponent } from './pages/clients-list/clients-list.component';
import { ClientRegisterComponent } from './pages/client-register/client-register.component';
import { WorkerRegisterComponent } from './pages/worker-register/worker-register.component';
import { ClientUpdateComponent } from './pages/client-update/client-update.component';
import { WorkerUpdateComponent } from './pages/worker-update/worker-update.component';

export const usersRoutes: Routes = [
  {
    path: 'socios',
    component: ClientsListComponent,
    data: {
      title: 'Socios',
      urls: [
        { title: 'Dashboard', url: '/dashboards/dashboard1' },
        { title: 'Socios' },
      ],
    },
  },
  {
    path: 'socios/registro',
    component: ClientRegisterComponent,
    data: {
      title: 'Registro de socio',
      urls: [
        { title: 'Dashboard', url: '/dashboards/dashboard1' },
        { title: 'Registro de socio' },
      ],
    },
  },
  {
    path: 'socios/:id/edicion',
    component: ClientUpdateComponent,
    data: {
      title: 'Edición de socio',
      urls: [
        { title: 'Dashboard', url: '/dashboards/dashboard1' },
        { title: 'Edición de socio' },
      ],
    },
  },
  {
    path: 'personal',
    component: WorkersListComponent,
    data: {
      title: 'Personal',
      urls: [
        { title: 'Dashboard', url: '/dashboards/dashboard1' },
        { title: 'Personal' },
      ],
    },
  },
  {
    path: 'personal/registro',
    component: WorkerRegisterComponent,
    data: {
      title: 'Registro de personal',
      urls: [
        { title: 'Dashboard', url: '/dashboards/dashboard1' },
        { title: 'Registro de personal' },
      ],
    },
  },
  {
    path: 'personal/:id/edicion',
    component: WorkerUpdateComponent,
    data: {
      title: 'Edición de personal',
      urls: [
        { title: 'Dashboard', url: '/dashboards/dashboard1' },
        { title: 'Edición de personal' },
      ],
    },
  },
];
