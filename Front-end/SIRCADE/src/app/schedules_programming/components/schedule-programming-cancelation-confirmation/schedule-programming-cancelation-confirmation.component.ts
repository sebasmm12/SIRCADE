import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AccountsService } from 'src/app/auth/services/accounts.service';
import { MaterialModule } from 'src/app/material.module';
import { SchedulesProgrammingService } from '../../services/schedules-programming.service';

@Component({
  selector: 'app-schedule-programming-cancelation-confirmation',
  standalone: true,
  imports: [MaterialModule],
  templateUrl: './schedule-programming-cancelation-confirmation.component.html',
  styleUrl: './schedule-programming-cancelation-confirmation.component.scss',
})
export class ScheduleProgrammingCancelationConfirmationComponent
  implements OnInit
{
  accountsService = inject(AccountsService);
  schedulesProgrammingService = inject(SchedulesProgrammingService);

  scheduleProgrammingId: number = inject(MAT_DIALOG_DATA);

  destroyRef = inject(DestroyRef);

  programmingType: string = '';

  constructor(
    public dialogRef: MatDialogRef<ScheduleProgrammingCancelationConfirmationComponent>
  ) {}

  ngOnInit(): void {
    if (this.accountsService.User.role != 'socio') {
      this.programmingType = 'programación';
      return;
    }
    this.programmingType = 'reserva';
  }

  cancel(): void {
    this.schedulesProgrammingService
      .cancel(this.scheduleProgrammingId)
      .subscribe(() => {
        this.dialogRef.close(this.programmingType);
      });
  }
}
