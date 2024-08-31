import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ScheduleProgrammingRequest } from '../interfaces/requests/schedule-programming.request';
import { Observable } from 'rxjs';
import { ScheduleProgrammingInfoResponse } from '../interfaces/responses/schedule-programming-info.response';
import { SchedulesProgrammingWeeklyQueries } from '../interfaces/queries/schedules-programming-weekly.queries';
import queryString from 'query-string';

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

  getAllByWeek(
    schedulesProgrammingWeeklyQueries: SchedulesProgrammingWeeklyQueries
  ): Observable<ScheduleProgrammingInfoResponse[]> {
    const params = queryString.stringify(schedulesProgrammingWeeklyQueries);

    return this.httpClient.get<ScheduleProgrammingInfoResponse[]>(
      `${this.baseUrl}/schedules-programming?${params}`
    );
  }
}
