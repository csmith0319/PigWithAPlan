import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastComponent } from './toast/toast.component';
import {
  MAT_SNACK_BAR_DEFAULT_OPTIONS,
  MatSnackBarModule,
} from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { A11yModule } from '@angular/cdk/a11y';
import { DatatableComponent } from './datatable/datatable.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@Angular/material/radio';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatExpansionModule } from '@angular/material/expansion';

import { CdkDropList, CdkDrag } from '@angular/cdk/drag-drop';
import { MatDialogModule } from '@angular/material/dialog';
import { SidebarComponent } from './nav/sidebar/sidebar.component';
import { NavLinkComponent } from './nav/nav-link/nav-link.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    ToastComponent,
    DatatableComponent,
    SidebarComponent,
    NavLinkComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,

    MatSnackBarModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatSidenavModule,
    MatInputModule,
    MatSelectModule,
    MatRadioModule,
    MatTableModule,
    MatTabsModule,
    MatExpansionModule,
    MatDialogModule,

    A11yModule,
    CdkDropList,
    CdkDrag,
  ],
  exports: [
    ToastComponent,
    DatatableComponent,
    SidebarComponent,
    NavLinkComponent,
  ],
  providers: [
    {
      provide: MAT_SNACK_BAR_DEFAULT_OPTIONS,
      useValue: {
        duration: 1000,
        horizontalPosition: 'center',
        verticalPosition: 'top',
        panelClass: 'toast-panel',
      },
    },
  ],
})
export class SharedModule {}
