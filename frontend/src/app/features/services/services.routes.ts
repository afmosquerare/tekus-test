import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/services/services.page'),
  },
  {
    path: '**',
    redirectTo: ''
  }
] satisfies Routes;
