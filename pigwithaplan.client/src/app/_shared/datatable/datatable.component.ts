import { SelectionModel } from '@angular/cdk/collections';
import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { DTColumn } from '../_models/DTColumn';

@Component({
  selector: 'app-datatable',
  templateUrl: './datatable.component.html',
  styleUrl: './datatable.component.scss',
})
export class DatatableComponent implements OnInit {
  @Input() data: any[] = [];
  @Input() columns: DTColumn[] = [];
  @Input() selectable: boolean = true;

  dataSource!: MatTableDataSource<any>;
  displayedColumns: string[] = [];
  selection = new SelectionModel<any>(true, []);

  ngOnInit() {
    this.dataSource = new MatTableDataSource(this.data);
    this.displayedColumns = this.selectable
      ? ['select', ...this.columns.map((column) => column.label)]
      : this.columns.map((column) => column.label);
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.data.forEach((row) => this.selection.select(row));
  }
}
