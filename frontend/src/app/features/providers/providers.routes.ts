import { Routes } from '@angular/router';

export default   [
  {
    path: '',
    loadComponent: () => import('./pages/providers/providers.page'),
  },
  {
    path: '**',
    redirectTo: ''
  }
] satisfies Routes;
