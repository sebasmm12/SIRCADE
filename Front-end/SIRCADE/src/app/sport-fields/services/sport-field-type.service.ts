import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SportFieldTypeResponse } from '../interfaces/responses/sport-field-type.response';

@Injectable({
  providedIn: 'root',
})
export class SportFieldTypeService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  getAll(): Observable<SportFieldTypeResponse[]> {
    return this.httpClient.get<SportFieldTypeResponse[]>(
      `${this.baseUrl}/sport-fields/types/all`
    );
  }
}
