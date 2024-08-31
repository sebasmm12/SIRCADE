import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { AccountsService } from '../services/accounts.service';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const accountsService = inject(AccountsService);

  if (!accountsService || accountsService.AccessToken === '') return next(req);

  const authenticatedRequest = req.clone({
    setHeaders: {
      Authorization: `Bearer ${accountsService.AccessToken}`,
    },
  });

  return next(authenticatedRequest).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401 || error.status === 403) {
        accountsService.logOut();
      }

      return throwError(() => new Error(error.message));
    })
  );
};
