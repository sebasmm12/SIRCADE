import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ClientInfoResponse } from '../interfaces/responses/client-info.response';

@Injectable({
  providedIn: 'root',
})
export class ClientsService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);
  constructor() {}

  getAll(): Observable<ClientInfoResponse[]> {
    return this.httpClient.get<ClientInfoResponse[]>(
      `${this.baseUrl}/clients/all`
    );
  }
}
