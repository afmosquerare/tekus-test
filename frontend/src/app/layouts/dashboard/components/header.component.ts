import { Component, output } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'dashboard-header',
  imports: [MatToolbarModule, MatIconModule, MatButtonModule],
  template: `
    <mat-toolbar class="shadow-sm flex justify-between">
      <button mat-icon-button (click)="onToggle.emit()">
        <mat-icon>menu</mat-icon>
      </button>
      <button (click)="logout()" mat-icon-button>
        <mat-icon>logout</mat-icon>

      </button>

      
    </mat-toolbar>
  `,
})
export class HeaderComponent {
  onToggle = output();
  logout(){
    console.log('Cerrar sesión');
  }
}