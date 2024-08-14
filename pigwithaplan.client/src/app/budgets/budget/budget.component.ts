import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { DTColumn } from '../../_shared/_models/DTColumn';
import { FormBuilder, Validators } from '@angular/forms';
import { CategoryGroupService } from '../../_services/category-group.service';
import { startWith, Subscription, switchMap } from 'rxjs';
import { ICategoryGroup } from '../../_models/category-group';
import { BudgetService } from '../../_services/budget.service';
import { ToastService } from '../../_shared/toast/toast.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { MatAccordion } from '@angular/material/expansion';

@Component({
  selector: 'app-budget',
  templateUrl: './budget.component.html',
  styleUrl: './budget.component.scss',
})
export class BudgetComponent implements OnInit, OnDestroy {
  @ViewChild(MatAccordion) accordion!: MatAccordion;

  constructor(
    private formBuilder: FormBuilder,
    private categoryGroupService: CategoryGroupService,
    private budgetService: BudgetService,
    private toastService: ToastService
  ) {}

  private subscription: Subscription = new Subscription();

  ngOnInit(): void {
    this.Load();
  }

  ngOnDestroy(): void {
    if (!this.subscription.closed) this.subscription.unsubscribe();
  }

  Load(): void {
    this.subscription.add(
      this.categoryGroupService.CategoryGroupData_changed.pipe(
        startWith(0),
        switchMap(() => {
          return this.categoryGroupService.getAll();
        })
      ).subscribe({
        next: (categoryGroups) => {
          this.categoryGroups = categoryGroups;
        },
        error: (err) => {},
      })
    );
  }

  isNewCategoryGroup: boolean = false;

  dtCategoryColumns: DTColumn[] = [
    { label: 'Item' },
    { label: 'Planned' },
    { label: 'Remaining' },
  ];

  dtTransactionsColumns: DTColumn[] = [
    { label: 'Date' },
    { label: 'Transaction' },
    { label: 'Amount' },
  ];

  categoryGroups: ICategoryGroup[] = [];

  categoryAData = [
    { Item: 'Budget item A', Planned: '$1,300', Remaining: '$800' },
    { Item: 'Budget item B', Planned: '$500', Remaining: '$0' },
    { Item: 'Budget item C', Planned: '$0', Remaining: '-$100' },
    { Item: 'Budget item C', Planned: '$0', Remaining: '-$100' },
    { Item: 'Budget item C', Planned: '$0', Remaining: '-$100' },
  ];

  transactionsData = [
    { Date: 'June', Transaction: 'Transaction 1', Amount: '-$600' },
    { Date: 'June', Transaction: 'Transaction 2', Amount: '-$100' },
    { Date: 'June', Transaction: 'Transaction 1', Amount: '$100' },
    { Date: 'May', Transaction: 'Transaction 1', Amount: '-$600' },
    { Date: 'May', Transaction: 'Transaction 2', Amount: '-$100' },
  ];

  getCategoryGroupDetails(_newCategoryGroup: ICategoryGroup) {
    _newCategoryGroup.name = this.categoryGroupName.value;
  }

  createCategoryGroup() {
    let _newCategoryGroup: ICategoryGroup = { id: 0 };
    this.getCategoryGroupDetails(_newCategoryGroup);

    if (this.formCategoryGroup.valid) {
      this.subscription.add(
        this.categoryGroupService.add(_newCategoryGroup).subscribe({
          next: (categoryGroup) => {
            if (categoryGroup) {
              this.toastService.showSuccess('Category Group Created');
              this.formCategoryGroup.reset();
              this.isNewCategoryGroup = false;
            }
          },
          error: (err) => {},
        })
      );
    } else {
    }
  }

  addCategoryGroup() {
    this.isNewCategoryGroup = !this.isNewCategoryGroup;
  }

  drop(event: CdkDragDrop<ICategoryGroup[]>) {
    moveItemInArray(
      this.categoryGroups,
      event.previousIndex,
      event.currentIndex
    );
  }

  formCategoryGroup = this.formBuilder.group({
    categoryGroupName: this.formBuilder.nonNullable.control<string>(null!, {
      validators: Validators.required,
    }),
  });

  get categoryGroupName() {
    return this.formCategoryGroup.controls.categoryGroupName;
  }
}
