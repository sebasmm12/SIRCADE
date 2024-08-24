import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RolePermissionDto } from '../interfaces/dtos/role-permission.dto';

@Injectable({
  providedIn: 'root',
})
export class PermissionsService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  getAll(): Observable<RolePermissionDto[]> {
    return this.httpClient.get<RolePermissionDto[]>(
      `${this.baseUrl}/permissions/all`
    );
  }
}
