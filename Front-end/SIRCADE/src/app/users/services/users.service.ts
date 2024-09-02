import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UsersQueries } from '../interfaces/queries/users.queries';
import { Observable } from 'rxjs';
import { DataTableResponse } from 'src/app/shared/interfaces/responses/data-table.response';
import { UserResponse } from '../interfaces/responses/user.response';
import queryString from 'query-string';
import { SelectorOptionDto } from 'src/app/shared/interfaces/dtos/selector-option.dto';
import { UserRegisterRequest } from '../interfaces/requests/user-register.request';
import { UserInfoResponse } from '../interfaces/responses/user-info.response';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  get(usersQueries: UsersQueries): Observable<DataTableResponse<UserResponse>> {
    const params = queryString.stringify(usersQueries);

    return this.httpClient.get<DataTableResponse<UserResponse>>(
      `${this.baseUrl}/users?${params}`
    );
  }

  getById(userId: number): Observable<UserInfoResponse> {
    return this.httpClient.get<UserInfoResponse>(
      `${this.baseUrl}/users/${userId}`
    );
  }

  getUnities(): Observable<SelectorOptionDto[]> {
    return this.httpClient.get<SelectorOptionDto[]>(
      `${this.baseUrl}/users/unities/all`
    );
  }

  register(userRegisterRequest: UserRegisterRequest): Observable<void> {
    return this.httpClient.post<void>(
      `${this.baseUrl}/users`,
      userRegisterRequest
    );
  }

  updateStatus(userId: number): Observable<void> {
    return this.httpClient.put<void>(
      `${this.baseUrl}/users/${userId}/status`,
      null
    );
  }

  update(userUpdateRequest: UserInfoResponse): Observable<void> {
    return this.httpClient.put<void>(
      `${this.baseUrl}/users`,
      userUpdateRequest
    );
  }
}
