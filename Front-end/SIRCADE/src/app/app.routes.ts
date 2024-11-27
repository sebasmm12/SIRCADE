import { Routes } from '@angular/router';
import { BlankComponent } from './layouts/blank/blank.component';
import { FullComponent } from './layouts/full/full.component';

export const routes: Routes = [
  {
    path: '',
    component: FullComponent,
    children: [
      {
        path: '',
        redirectTo: 'principal',
        pathMatch: 'full',
      },
      {
        path: 'starter',
        loadChildren: () =>
          import('./pages/pages.routes').then((m) => m.PagesRoutes),
      },
      {
        path: 'dashboards',
        loadChildren: () =>
          import('./pages/dashboards/dashboards.routes').then(
            (m) => m.DashboardsRoutes
          ),
      },
      {
        path: 'ui-components',
        loadChildren: () =>
          import('./pages/ui-components/ui-components.routes').then(
            (m) => m.UiComponentsRoutes
          ),
      },
      {
        path: 'forms',
        loadChildren: () =>
          import('./pages/forms/forms.routes').then((m) => m.FormsRoutes),
      },
      {
        path: 'charts',
        loadChildren: () =>
          import('./pages/charts/charts.routes').then((m) => m.ChartsRoutes),
      },
      {
        path: 'apps',
        loadChildren: () =>
          import('./pages/apps/apps.routes').then((m) => m.AppsRoutes),
      },
      {
        path: 'widgets',
        loadChildren: () =>
          import('./pages/widgets/widgets.routes').then((m) => m.WidgetsRoutes),
      },
      {
        path: 'tables',
        loadChildren: () =>
          import('./pages/tables/tables.routes').then((m) => m.TablesRoutes),
      },
      {
        path: 'datatable',
        loadChildren: () =>
          import('./pages/datatable/datatable.routes').then(
            (m) => m.DatatablesRoutes
          ),
      },
      {
        path: 'theme-pages',
        loadChildren: () =>
          import('./pages/theme-pages/theme-pages.routes').then(
            (m) => m.ThemePagesRoutes
          ),
      },
      {
        path: 'roles',
        loadChildren: () =>
          import('./roles/roles.routes').then((m) => m.roleRoutes),
      },
      {
        path: 'canchas-deportivas',
        loadChildren: () =>
          import('./sport-fields/sport-fields.routes').then(
            (m) => m.sportFieldsRoutes
          ),
      },
      {
        path: '',
        loadChildren: () =>
          import('./schedules_programming/schedules_programming.routes').then(
            (m) => m.schedulesProgrammingRoutes
          ),
      },
      {
        path: '',
        loadChildren: () =>
          import('./users/users.routes').then((m) => m.usersRoutes),
      },
      {
        path: 'principal',
        loadChildren: () =>
          import('./dashboards/dashboards.routes').then(
            (m) => m.dashboardRoutes
          ),
      },
      {
        path: 'reportes',
        loadChildren: () =>
          import('./reports/reports.routes').then((m) => m.routes),
      },
      {
        path: 'seguridad',
        loadChildren: () =>
          import('./auth/auth.routes').then((m) => m.internalAuthRoutes),
      }
    ],
  },
  {
    path: '',
    component: BlankComponent,
    children: [
      {
        path: 'authentication',
        loadChildren: () =>
          import('./pages/authentication/authentication.routes').then(
            (m) => m.AuthenticationRoutes
          ),
      },
      {
        path: 'landingpage',
        loadChildren: () =>
          import('./pages/theme-pages/landingpage/landingpage.routes').then(
            (m) => m.LandingPageRoutes
          ),
      },
      {
        path: '',
        loadChildren: () =>
          import('./auth/auth.routes').then((m) => m.authRoutes),
      },
    ],
  },
  {
    path: '**',
    redirectTo: 'authentication/error',
  },
];
