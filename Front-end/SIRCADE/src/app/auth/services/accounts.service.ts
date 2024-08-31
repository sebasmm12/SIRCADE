import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AccountCredentialsRequest } from '../interfaces/requests/account-credentials.request';
import { AccountInfoResponse } from '../interfaces/responses/account-info.response';
import { AccountDto } from '../interfaces/dtos/account.dto';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountsService {
  private baseUrl: string = environment.apiUrl;
  private accessToken: string = '';
  private user!: AccountDto;
  private jwtHelperService: JwtHelperService;

  private httpClient: HttpClient = inject(HttpClient);
  private router = inject(Router);

  constructor() {
    this.jwtHelperService = new JwtHelperService();
    this.accessToken = this.getToken();

    if (this.accessToken) this.user = this.getUser();
  }

  getToken(): string {
    return localStorage.getItem('accessToken') ?? '';
  }

  get User(): AccountDto {
    return { ...this.user };
  }

  get AccessToken(): string {
    return this.accessToken;
  }

  saveToken(token: string): void {
    localStorage.setItem('accessToken', token);
    this.accessToken = token;
  }

  getUser(): AccountDto {
    const decodedUser = this.jwtHelperService.decodeToken(this.accessToken);

    const user: AccountDto = {
      id: decodedUser[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ],
      email:
        decodedUser[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
        ],
      role: decodedUser[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ],
      roleId: decodedUser['RoleId'],
      name: decodedUser[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
      ],
    };

    return user;
  }

  login(request: AccountCredentialsRequest): Observable<AccountInfoResponse> {
    return this.httpClient
      .post<AccountInfoResponse>(`${this.baseUrl}/accounts/login`, request)
      .pipe(
        tap((response) => {
          this.saveToken(response.token);
          this.user = this.getUser();
        })
      );
  }

  logOut(): void {
    localStorage.removeItem('accessToken');
    this.accessToken = '';
    this.user = {} as AccountDto;

    this.router.navigate(['/inicio-sesion']);
  }
}
