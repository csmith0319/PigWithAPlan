import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Inject,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { IBudget } from '../../_models/budget';
import { Subscription } from 'rxjs';
import { BudgetService } from '../_services/budget.service';
import { ToastService } from 'app/_shared/toast/toast.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-budget',
  templateUrl: './create-budget.component.html',
  styleUrl: './create-budget.component.scss',
})
export class CreateBudgetComponent implements OnInit, OnDestroy {
  @Output() formClosed = new EventEmitter<void>();

  colors = [
    { name: 'Yellow', value: '#fee327' },
    { name: 'Light Orange', value: '#fdca54' },
    { name: 'Orange', value: '#f6a570' },
    { name: 'Light Pink', value: '#f1969b' },
    { name: 'Pink', value: '#f08ab1' },
    { name: 'Light Purple', value: '#c78dbd' },
    { name: 'Purple', value: '#927db6' },
    { name: 'Light Blue', value: '#5da0d7' },
    { name: 'Blue', value: '#00b3e1' },
    { name: 'Teal', value: '#50bcbf' },
    { name: 'Light Green', value: '#65bda5' },
    { name: 'Green', value: '#87bf54' },
  ];

  constructor(
    private formBuilder: FormBuilder,
    private budgetService: BudgetService,
    private toastService: ToastService,
    private cdr: ChangeDetectorRef,
    public dialogRef: MatDialogRef<CreateBudgetComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string
  ) {}

  private subscription: Subscription = new Subscription();

  check(): void {
    this.cdr.detectChanges();
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    if (!this.subscription.closed) this.subscription.unsubscribe();
  }

  handleFormClose(): void {
    this.dialogRef.close();
  }

  getDetails(_newBudget: IBudget) {
    _newBudget.name = this.name.value;
    _newBudget.description = this.description.value;
    _newBudget.color = this.color.value;
  }

  create(): void {
    let _newBudget = { id: 0 };
    this.getDetails(_newBudget);

    this.subscription.add(
      this.budgetService.add(_newBudget).subscribe((response) => {
        if (response) {
          this.toastService.showSuccess('Budget was successfully created.');
          this.dialogRef.close();
        }
      })
    );
  }

  submit(): void {
    if (this.formBudget.valid) {
      this.create();
    } else {
      console.log('Invalid form.');
    }
  }

  cancel(): void {
    this.dialogRef.close();
  }

  formBudget = this.formBuilder.group({
    name: this.formBuilder.nonNullable.control<string>('', {
      validators: Validators.required,
    }),
    description: this.formBuilder.nonNullable.control<string>(null!),
    color: this.formBuilder.nonNullable.control<string>(null!),
  });

  get name(): FormControl {
    return this.formBudget.controls.name;
  }

  get description(): FormControl {
    return this.formBudget.controls.description;
  }

  get color(): FormControl {
    return this.formBudget.controls.color;
  }
}
