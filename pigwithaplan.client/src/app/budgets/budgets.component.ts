import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { BudgetService } from '../_services/budget.service';
import { ToastService } from '../_shared/toast/toast.service';
import { FormBuilder } from '@angular/forms';
import { IBudget } from '../_models/budget';
import { startWith, Subscription, switchMap } from 'rxjs';
import { CreateBudgetComponent } from './create-budget/create-budget.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-budgets',
  templateUrl: './budgets.component.html',
  styleUrls: ['./budgets.component.scss'],
})
export class BudgetsComponent implements OnInit, OnDestroy {
  isCreatingBudget = false;
  budgets: IBudget[] = [];

  private subscription = new Subscription();

  constructor(
    private budgetService: BudgetService,
    private toastService: ToastService,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.Load();
  }

  showForm(): void {
    const dialogRef = this.dialog.open(CreateBudgetComponent);

    dialogRef.afterClosed().subscribe({
      next: (result) => {
        console.log('Result:', result);
      },
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
}
