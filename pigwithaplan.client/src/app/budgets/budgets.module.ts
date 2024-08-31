import { CreateBudgetComponent } from './create-budget/create-budget.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'app/_shared/shared.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { BudgetListComponent } from './list/budget-list.component';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { BudgetComponent } from './budget/budget.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BudgetRoutesModule } from './budgets.routing';
import { A11yModule } from '@angular/cdk/a11y';
import { CdkDrag, CdkDropList } from '@angular/cdk/drag-drop';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  imports: [
    CommonModule,
    BudgetRoutesModule,
    SharedModule,

    FormsModule,
    ReactiveFormsModule,

    MatCardModule,
    MatIconModule,
    MatFormFieldModule,
    MatExpansionModule,
    MatTabsModule,
    MatSelectModule,
    MatDialogModule,
    MatInputModule,
    MatButtonModule,

    A11yModule,
    CdkDropList,
    CdkDrag,
  ],
  declarations: [CreateBudgetComponent, BudgetListComponent, BudgetComponent],
})
export class BudgetsModule {}
