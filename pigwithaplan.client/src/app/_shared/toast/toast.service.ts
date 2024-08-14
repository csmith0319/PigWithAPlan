import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ToastComponent } from './toast.component';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  constructor(private snackBar: MatSnackBar) {}

  showSuccess(message: string) {
    this.snackBar.openFromComponent(ToastComponent, {
      data: { message: message, action: this.snackBar.dismiss },
      panelClass: ['border', 'border-green-500', 'rounded'],
    });
  }

  showError(message: string) {
    this.snackBar.openFromComponent(ToastComponent, {
      data: { message: message, action: this.snackBar.dismiss },
      panelClass: ['border', 'border-red-500', 'rounded'],
    });
  }
}
