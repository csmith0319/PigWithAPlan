import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { DTColumn } from '../../_shared/_models/DTColumn';
import { FormBuilder, Validators } from '@angular/forms';
import { CategoryGroupService } from '../_services/category-group.service';
import { combineLatest, of, startWith, Subscription, switchMap } from 'rxjs';
import { ICategoryGroup } from '../../_models/category-group';
import { ToastService } from '../../_shared/toast/toast.service';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { MatAccordion } from '@angular/material/expansion';
import { BudgetService } from '../_services/budget.service';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from '../_services/category.service';
import { ICategory } from '../_models/Category';

@Component({
  selector: 'app-budget',
  templateUrl: './budget.component.html',
  styleUrl: './budget.component.scss',
})
export class BudgetComponent implements OnInit, OnDestroy {
  @ViewChild(MatAccordion) accordion!: MatAccordion;

  budgetId: number = 0;

  constructor(
    private formBuilder: FormBuilder,
    private categoryGroupService: CategoryGroupService,
    private budgetService: BudgetService,
    private route: ActivatedRoute,
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
      combineLatest([
        this.route.params,
        this.categoryGroupService.CategoryGroupData_changed.pipe(startWith(0)),
        // this.categoryService.CategoryData_changed.pipe(startWith(0))
      ])
        .pipe(
          switchMap(([params, categoryGroups]) => {
            this.budgetId = Number(params['id']);

            return this.budgetId > 0
              ? this.categoryGroupService.getAll(this.budgetId)
              : of([]);
          })
        )
        .subscribe({
          next: (categoryGroups) => {
            this.categoryGroups = categoryGroups;

            this.categoryGroups.forEach((x) => {
              this.populateDatatableRows(x.categories ?? []);
            });
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

  dtCategoryRows: any[] = [];
  dtTransactionRows: any[] = [];

  getCategoryGroupDetails(_newCategoryGroup: ICategoryGroup) {
    _newCategoryGroup.name = this.categoryGroupName.value;
  }

  createCategoryGroup() {
    let _newCategoryGroup: ICategoryGroup = { id: 0, budgetId: this.budgetId };
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

  populateDatatableRows(data: ICategory[]): void {
    data.forEach((x) => {
      const categoryGroupId: number = Number(x.categoryGroupId);

      if (!isNaN(categoryGroupId)) {
        if (this.dtCategoryRows[categoryGroupId] == undefined)
          this.dtCategoryRows[categoryGroupId] = [];
        this.dtCategoryRows[categoryGroupId].push({
          Item: x.name,
          Planned: '$0',
          Remaining: '$0',
        });
      }
    });
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
