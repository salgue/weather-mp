import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SpinnerDialogComponent } from 'src/app/shared/spinner-dialog/spinner-dialog.component';

@Injectable({
  providedIn: 'root',
})
export class SpinnerService {
  spinnerDialog: MatDialogRef<SpinnerDialogComponent> | undefined;
  constructor(private matDialog: MatDialog) {}

  startSpinner() {
    this.spinnerDialog = this.matDialog.open(SpinnerDialogComponent, {
      panelClass: 'transparent',
      disableClose: true,
      autoFocus: false,
    });
  }

  closeSpinner() {
    if (this.spinnerDialog) {
      this.spinnerDialog.close();
    }
  }
}
