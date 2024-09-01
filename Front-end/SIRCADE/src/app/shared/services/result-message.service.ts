import { inject, Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ResultActionType } from '../interfaces/enums/result-action-type';

@Injectable({
  providedIn: 'root',
})
export class ResultMessageService {
  snackBarService = inject(MatSnackBar);

  private resultMessageClass = '';

  private resultActionClasses: Record<ResultActionType, string> = {
    [ResultActionType.Register]: 'snackbar-success',
    [ResultActionType.Update]: 'snackbar-warning',
    [ResultActionType.Deletion]: 'snackbar-error',
  };

  constructor() {}

  showMessage(
    message: string,
    action: ResultActionType,
    duration: number = 2000
  ): void {
    this.snackBarService.open(message, 'Cerrar', {
      panelClass: [this.resultActionClasses[action]],
      duration: duration,
    });
  }
}
