import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ScheduleProgrammingRequest } from '../interfaces/requests/schedule-programming.request';

@Injectable({
  providedIn: 'root',
})
export class SchedulesProgrammingService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  register(request: ScheduleProgrammingRequest) {
    return this.httpClient.post(
      `${this.baseUrl}/schedules-programming`,
      request
    );
  }
}
