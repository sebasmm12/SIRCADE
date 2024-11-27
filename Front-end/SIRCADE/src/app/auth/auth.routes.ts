import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { PasswordUpdateComponent } from './pages/password-update/password-update.component';

export const authRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'inicio-sesion',
        component: LoginComponent,
      },
    ],
  },
];

export const internalAuthRoutes: Routes = [{
  path: 'cambio-contrasena',
  component: PasswordUpdateComponent,
  data: {
    title: 'Configuración de cuenta',
    urls: [
      { title: 'Dashboard', url: '/principal' },
      { title: 'Cambio de contraseña' },
    ],
  }
}];
