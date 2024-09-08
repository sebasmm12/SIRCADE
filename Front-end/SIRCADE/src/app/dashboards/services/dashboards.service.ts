import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DashboardWidgetsResponse } from '../interfaces/responses/dashboard-widgets.response';
import { ScheduleProgrammingState } from 'src/app/schedules_programming/interfaces/enums/schedule-programming-state.enum';
import { DashboardResponse } from '../interfaces/responses/dashboard.response';

@Injectable({
  providedIn: 'root',
})
export class DashboardsService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  getWidgets(): Observable<DashboardWidgetsResponse> {
    return this.httpClient.get<DashboardWidgetsResponse>(
      `${this.baseUrl}/dashboards/widgets`
    );
  }

  getReservations(
    status: ScheduleProgrammingState | null = null
  ): Observable<DashboardResponse[]> {
    let url: string = `${this.baseUrl}/dashboards/reservations`;

    if (status) {
      url += `?reservationState=${status}`;
    }

    return this.httpClient.get<DashboardResponse[]>(url);
  }

  getReservationsByGrade(): Observable<DashboardResponse[]> {
    return this.httpClient.get<DashboardResponse[]>(
      `${this.baseUrl}/dashboards/reservations/grades`
    );
  }

  getReservationsByMonth(): Observable<DashboardResponse[]> {
    return this.httpClient.get<DashboardResponse[]>(
      `${this.baseUrl}/dashboards/reservations/months`
    );
  }
}
