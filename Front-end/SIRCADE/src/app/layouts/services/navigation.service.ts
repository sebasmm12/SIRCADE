import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RolePermissionResponse } from '../interfaces/responses/role-permission.response';

@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  getRolePermissions(roleId: number): Observable<RolePermissionResponse[]> {
    return this.httpClient.get<RolePermissionResponse[]>(
      `${this.baseUrl}/permissions/roles/${roleId}`
    );
  }
}
