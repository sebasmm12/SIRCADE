import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SportFieldsQueries } from '../interfaces/queries/sport-fields.queries';
import { DataTableResponse } from 'src/app/shared/interfaces/responses/data-table.response';
import queryString from 'query-string';
import { SportFieldResponse } from '../interfaces/responses/sport-field.response';
import { Observable } from 'rxjs';
import { SportFieldUpdateRequest } from '../interfaces/requests/sport-field-update.request';
import { SportFieldRegisterRequest } from '../interfaces/requests/sport-field-register.request';
import { SportFieldInfoResponse } from '../interfaces/responses/sport-field-info.response';

@Injectable({
  providedIn: 'root',
})
export class SportFieldsService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  get(
    sportFieldsQueries: SportFieldsQueries
  ): Observable<DataTableResponse<SportFieldResponse>> {
    const params = queryString.stringify(sportFieldsQueries);

    return this.httpClient.get<DataTableResponse<SportFieldResponse>>(
      `${this.baseUrl}/sport-fields?${params}`
    );
  }

  getAll(): Observable<SportFieldInfoResponse[]> {
    return this.httpClient.get<SportFieldInfoResponse[]>(
      `${this.baseUrl}/sport-fields/all`
    );
  }

  getById(sportFieldId: number): Observable<SportFieldInfoResponse> {
    return this.httpClient.get<SportFieldInfoResponse>(
      `${this.baseUrl}/sport-fields/${sportFieldId}`
    );
  }

  update(sportFieldUpdateRequest: SportFieldUpdateRequest): Observable<void> {
    return this.httpClient.put<void>(
      `${this.baseUrl}/sport-fields`,
      sportFieldUpdateRequest
    );
  }

  register(
    sportFieldRegisterRequest: SportFieldRegisterRequest
  ): Observable<void> {
    return this.httpClient.post<void>(
      `${this.baseUrl}/sport-fields`,
      sportFieldRegisterRequest
    );
  }
}
