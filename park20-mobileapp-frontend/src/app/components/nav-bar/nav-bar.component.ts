import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css',
})
export class NavBarComponent implements OnInit {
  items: MenuItem[] | undefined;

  ngOnInit() {
    this.items = [
      {
        icon: 'pi pi-home',
        routerLink: '/home',
      },
      {
        icon: 'pi pi-user',
        routerLink: '/my-account',
      },
      {
        icon: 'pi pi-power-off',
        routerLink: '/login',
      },
    ];
  }
}
