import { Routes } from '@angular/router';

export default[
  {
    path: '',
    loadComponent: () => import('./pages/dashboard/dashboard.page'),
  },
  {
    path: '**',
    redirectTo: ''
  }
] satisfies Routes;
