import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>
  ) { }

  // Método chamado quando o usuário confirma a ação
  onConfirm(): void {
    this.dialogRef.close(true);
  }

  // Método chamado quando o usuário cancela a ação
  onCancel(): void {
    this.dialogRef.close(false);
  }
}
