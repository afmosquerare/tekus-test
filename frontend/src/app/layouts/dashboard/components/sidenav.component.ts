import { Component, input, signal } from "@angular/core";
import { MatIconModule } from "@angular/material/icon";
import { RouterLink, RouterLinkActive } from "@angular/router";

interface MenuItem {
  path: string;
  icon: string;
  label: string;
}

@Component({
  selector: 'dashboard-sidenav',
  standalone: true,
  imports: [MatIconModule, RouterLink, RouterLinkActive],
  template: `
    <div
      class="flex flex-col h-full bg-white border-r border-gray-200 transition-all duration-300"
    >
      <div class="flex items-center justify-center h-16 border-b border-gray-200">
        <h2
          class="text-xl font-bold text-blue-600 truncate "
          [class.text-center]="collapsed()"
        >
          {{ collapsed() ? 'T' : 'Tekus test' }}
        </h2>
      </div>

      <ul class="flex-1 p-2 flex flex-col gap-2">
        @for (item of items(); track item.label) {
          <li>
            <a
              class="flex items-center gap-3 p-3 rounded-lg hover:bg-blue-50
                     transition-colors text-gray-700"
              [routerLink]="item.path"
              routerLinkActive="bg-blue-50 text-blue-600"
              [class.justify-center]="collapsed()"
            >
              <mat-icon class="!text-[20px]">{{ item.icon }}</mat-icon>
              @if (!collapsed()) {
                <span class="truncate">{{ item.label }}</span>
              }
            </a>
          </li>
        }
      </ul>
    </div>
  `,
})
export class SidenavComponent {
  items = signal<MenuItem[]>([
    { path: '/dashboard', icon: 'dashboard', label: 'Dashboard' },
    { path: '/providers', icon: 'business', label: 'Proveedores' },
    { path: '/services', icon: 'build', label: 'Servicios' },
  ]);

  collapsed = input.required<boolean>();
}
