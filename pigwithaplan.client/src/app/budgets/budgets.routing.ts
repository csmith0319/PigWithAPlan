import { Routes, RouterModule } from '@angular/router';
import { BudgetListComponent } from './list/budget-list.component';
import { BudgetComponent } from './budget/budget.component';
import { AuthGuard } from 'app/auth/auth.guard';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: 'budgets',
    component: BudgetListComponent,
    canActivate: [AuthGuard],
  },
  { path: 'budgets/:id', component: BudgetComponent, canActivate: [AuthGuard] },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BudgetRoutesModule {}
