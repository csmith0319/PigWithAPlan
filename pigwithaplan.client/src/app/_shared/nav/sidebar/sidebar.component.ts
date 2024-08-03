import { Component, OnInit } from '@angular/core';
import { SidebarLink } from '../../../_models/sidebar-link.model';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
  links: SidebarLink[] = [
    { label: 'Home', path: '/home' },
    { label: 'About', path: '/about' },
    {
      label: 'Budgets',
      path: '/budgets',
      children: [
        {
          label: 'Budget A',
          path: '/budget-a',
          children: [
            { label: 'Accounts', path: '/budget-a/accounts' },
            { label: 'Transactions', path: '/budget-a/transactions' },
            { label: 'Settings', path: '/budget-a/settings' },
          ],
        },
      ],
    },
  ];

  constructor() {}

  ngOnInit(): void {}
}
