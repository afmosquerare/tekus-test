import { Component, computed, signal, ViewChild } from '@angular/core';
import { MatSidenavModule, MatSidenav } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { SidenavComponent } from './components/sidenav.component';
import { HeaderComponent } from './components/header.component';

@Component({
  selector: 'dashboard-layout',
  standalone: true,
  imports: [
    MatSidenavModule,
    MatIconModule,
    MatButtonModule,
    RouterModule,
    SidenavComponent,
    HeaderComponent
  ],
  template: `<mat-sidenav-container class="h-screen">
      <mat-sidenav
        class="!bg-black/5 !rounded-none border-r !border-gray-200 custom-animation"
        [style.width.px]="width()"
        opened
        mode="side"
      >
        <dashboard-sidenav [collapsed]="collapsed()" />
      </mat-sidenav>
      <mat-sidenav-content [style.margin-left.px]="width()">
        <dashboard-header (onToggle)="collapsed.set(!collapsed())" />
        <div class="p-4"><router-outlet /></div>
      </mat-sidenav-content>
    </mat-sidenav-container>`,
})
export default class DashboardLayout {
  collapsed = signal(false);
  width = computed(() => (this.collapsed() ? 64 : 200));

  
}
