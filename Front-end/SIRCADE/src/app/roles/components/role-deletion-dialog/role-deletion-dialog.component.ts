import { Component, inject } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { RoleResponse } from '../../interfaces/responses/role.response';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-role-deletion-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './role-deletion-dialog.component.html',
  styleUrl: './role-deletion-dialog.component.scss',
})
export class RoleDeletionDialogComponent {
  role: RoleResponse = inject(MAT_DIALOG_DATA);

  constructor(public dialogRef: MatDialogRef<RoleDeletionDialogComponent>) {}

  delete(): void {
    this.dialogRef.close(this.role.id);
  }
}
