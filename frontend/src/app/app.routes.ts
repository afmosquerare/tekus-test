import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'auth',
        loadChildren: ()=> import('./features/auth/auth.routes')
    },
    {
        path: '',
        loadComponent: ()=> import('./layouts/dashboard/dashboard.layout'),
        children: [
            {
                path: 'dashboard',
                loadChildren: ()=> import('./features/dashboard/dashboard.routes')
            },
            {
                path: 'services',
                loadChildren: ()=> import('./features/services/services.routes')
            },
            {
                path: 'providers',
                loadChildren: ()=> import('./features/providers/providers.routes')
            },
        ]
    }
];
