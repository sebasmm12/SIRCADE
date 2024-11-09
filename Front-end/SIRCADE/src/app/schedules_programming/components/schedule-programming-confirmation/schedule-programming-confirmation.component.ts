import { Component, DestroyRef, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MaterialModule } from 'src/app/material.module';

@Component({
  selector: 'app-schedule-programming-confirmation',
  standalone: true,
  imports: [MaterialModule],
  templateUrl: './schedule-programming-confirmation.component.html',
  styleUrl: './schedule-programming-confirmation.component.scss',
})
export class ScheduleProgrammingConfirmationComponent {
  destroyRef = inject(DestroyRef);

  totalOverlapped: number = inject(MAT_DIALOG_DATA);

  constructor(
    public dialogRef: MatDialogRef<ScheduleProgrammingConfirmationComponent>
  ) {}

  save(): void {
    this.dialogRef.close(true);
  }
}
