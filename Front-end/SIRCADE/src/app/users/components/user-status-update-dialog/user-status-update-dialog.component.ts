import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { UserResponse } from '../../interfaces/responses/user.response';
import { UsersService } from '../../services/users.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-user-status-update-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './user-status-update-dialog.component.html',
  styleUrl: './user-status-update-dialog.component.scss',
})
export class UserStatusUpdateDialogComponent implements OnInit {
  usersService = inject(UsersService);

  user: UserResponse = inject(MAT_DIALOG_DATA);
  destroyRef = inject(DestroyRef);

  dialogTitle: string = '';
  dialogContent: string = '';
  confirmationButtonText: string = '';

  contentMessages: Record<string, Map<boolean, string>> = {
    ['Socio']: new Map([
      [true, '¿Estás seguro que deseas desafiliar este socio?'],
      [false, '¿Estás seguro que deseas afiliar este socio?'],
    ]),
    ['Personal']: new Map([
      [true, '¿Estás seguro que deseas desactivar este personal?'],
      [false, '¿Estás seguro que deseas activar este personal?'],
    ]),
  };

  titleMessages: Record<string, Map<boolean, string>> = {
    ['Socio']: new Map([
      [true, 'Desafiliar socio'],
      [false, 'Afiliar socio'],
    ]),
    ['Personal']: new Map([
      [true, 'Desactivar personal'],
      [false, 'Activar personal'],
    ]),
  };

  buttonTextMessage: Record<string, Map<boolean, string>> = {
    ['Socio']: new Map([
      [true, 'Desafiliar'],
      [false, 'Afiliar'],
    ]),
    ['Personal']: new Map([
      [true, 'Desactivar'],
      [false, 'Activar'],
    ]),
  };

  userRole: string = '';

  constructor(
    public dialogRef: MatDialogRef<UserStatusUpdateDialogComponent>
  ) {}

  updateStatus(): void {
    this.usersService
      .updateStatus(this.user.id)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        const userInfo = {
          id: this.user.id,
          role: this.userRole,
        };
        this.dialogRef.close(userInfo);
      });
  }

  ngOnInit(): void {
    this.userRole = this.user.role == 'Socio' ? 'Socio' : 'Personal';

    this.dialogTitle = this.titleMessages[this.userRole].get(this.user.active)!;
    this.dialogContent = this.contentMessages[this.userRole].get(
      this.user.active
    )!;
    this.confirmationButtonText = this.buttonTextMessage[this.userRole].get(
      this.user.active
    )!;
  }
}
