import { Routes } from '@angular/router';

export default [
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login.page'),
  },
  {
    path: '**',
    redirectTo: 'login'
  }
] satisfies Routes;

