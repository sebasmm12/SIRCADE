import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RoleResponse } from '../interfaces/responses/role.response';
import { RolesQueries } from '../interfaces/queries/roles.queries';
import { DataTableResponse } from 'src/app/shared/interfaces/responses/data-table.response';
import queryString from 'query-string';
import { RoleInfoResponse } from '../interfaces/responses/role-info.response';
import { RoleUpdateRequest } from '../interfaces/requests/role-update.request';
import { RoleRegisterRequest } from '../interfaces/requests/role-register.request';

@Injectable({
  providedIn: 'root',
})
export class RolesService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  getAll(): Observable<RoleResponse[]> {
    return this.httpClient.get<RoleResponse[]>(`${this.baseUrl}/roles/all`);
  }

  get(rolesQueries: RolesQueries): Observable<DataTableResponse<RoleResponse>> {
    const params = queryString.stringify(rolesQueries);

    return this.httpClient.get<DataTableResponse<RoleResponse>>(
      `${this.baseUrl}/roles?${params}`
    );
  }

  getById(roleId: number): Observable<RoleInfoResponse> {
    return this.httpClient.get<RoleInfoResponse>(
      `${this.baseUrl}/roles/${roleId}`
    );
  }

  delete(id: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.baseUrl}/roles/${id}/status`);
  }

  update(roleUpdateRequest: RoleUpdateRequest): Observable<void> {
    return this.httpClient.put<void>(
      `${this.baseUrl}/roles`,
      roleUpdateRequest
    );
  }

  register(roleUpdateRequest: RoleRegisterRequest): Observable<void> {
    return this.httpClient.post<void>(
      `${this.baseUrl}/roles`,
      roleUpdateRequest
    );
  }
}
