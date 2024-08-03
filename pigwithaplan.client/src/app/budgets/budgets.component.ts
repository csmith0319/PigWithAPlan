import { Component, OnInit } from '@angular/core';
import { BudgetService } from '../_services/budget.service';

@Component({
  selector: 'app-budgets',
  templateUrl: './budgets.component.html',
  styleUrls: ['./budgets.component.css'],
})
export class BudgetsComponent implements OnInit {
  budgets = [
    { name: 'Budget Name', used: '4 hours ago', favorite: false },
    { name: 'Budget Name', used: '4 hours ago', favorite: false },
    { name: 'Budget Name', used: '4 hours ago', favorite: false },
    { name: 'Budget Name', used: '4 hours ago', favorite: false },
    { name: 'Budget Name', used: '4 hours ago', favorite: false },
    { name: 'Budget Name', used: '4 hours ago', favorite: false },
    { name: 'Budget Name', used: '4 hours ago', favorite: false },
    { name: 'Budget Name', used: '4 hours ago', favorite: true },
  ];

  constructor(private budgetService: BudgetService) {}

  ngOnInit(): void {}

  toggleFavorite(budget: any): void {
    budget.favorite = !budget.favorite;
  }
}
