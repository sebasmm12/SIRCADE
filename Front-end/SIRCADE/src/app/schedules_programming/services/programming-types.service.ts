import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProgrammingTypeInfoResponse } from '../interfaces/responses/programming-type-info.response';

@Injectable({
  providedIn: 'root',
})
export class ProgrammingTypesService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  getAll(): Observable<ProgrammingTypeInfoResponse[]> {
    return this.httpClient.get<ProgrammingTypeInfoResponse[]>(
      `${this.baseUrl}/programming/types/all`
    );
  }
}
