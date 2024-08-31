import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { startWith, Subscription, switchMap } from 'rxjs';
import { CreateBudgetComponent } from '../create-budget/create-budget.component';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';
import { ToastService } from 'app/_shared/toast/toast.service';
import { BudgetService } from '../_services/budget.service';
import { IBudget } from 'app/_models/budget';
import { Router } from '@angular/router';

@Component({
  selector: 'app-budget-list',
  templateUrl: './budget-list.component.html',
  styleUrls: ['./budget-list.component.css'],
})
export class BudgetListComponent implements OnInit {
  isCreatingBudget = false;
  budgets: IBudget[] = [];

  private subscription = new Subscription();

  constructor(
    private budgetService: BudgetService,
    private toastService: ToastService,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef,
    private dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.Load();
  }

  showForm(): void {
    const dialogRef = this.dialog.open(CreateBudgetComponent);

    dialogRef.afterClosed().subscribe({
      next: (result) => {},
      error: () => {},
    });
  }

  check(): void {
    this.cdr.detectChanges();
  }

  Load(): void {
    this.subscription.add(
      this.budgetService.BudgetData_changed.pipe(
        startWith(0),
        switchMap(() => {
          return this.budgetService.getAll();
        })
      ).subscribe({
        next: (budgets) => {
          this.budgets = budgets;

          this.check();
        },
        error: (error) => {
          this.toastService.showError('Budget List Retrieval Failed');
        },
      })
    );
  }

  ngOnDestroy(): void {
    if (!this.subscription.closed) this.subscription.unsubscribe();
  }

  toggleFavorite(id: number): void {
    this.subscription.add(
      this.budgetService.favorite(id).subscribe((response) => {})
    );
  }

  navigateToBudget(id: number): void {
    this.router.navigate([`/budgets/${id}`]);
  }
}
