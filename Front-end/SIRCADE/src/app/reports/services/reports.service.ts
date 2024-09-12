import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FrequentlyUsersQueries } from '../interfaces/queries/frequently-users.queries';
import queryString from 'query-string';
import { DataTableResponse } from '../../shared/interfaces/responses/data-table.response';
import { Observable } from 'rxjs';
import { ReservationInfoResponse } from '../interfaces/responses/reservation-info.response';

@Injectable({
  providedIn: 'root',
})
export class ReportsService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  getFrequentlyUsers(
    frequentlyUsersQueries: FrequentlyUsersQueries
  ): Observable<DataTableResponse<ReservationInfoResponse>> {
    const params = queryString.stringify(frequentlyUsersQueries);

    let url = `${this.baseUrl}/reports/frequently-users?${params}`;

    return this.httpClient.get<DataTableResponse<ReservationInfoResponse>>(url);
  }

  getCancelledReservationsByUser(
    frequentlyUsersQueries: FrequentlyUsersQueries
  ) {
    const params = queryString.stringify(frequentlyUsersQueries);

    let url = `${this.baseUrl}/reports/cancelled-reservations-by-user?${params}`;

    return this.httpClient.get(url);
  }

  getMonthlyReservations(): Observable<
    DataTableResponse<ReservationInfoResponse>
  > {
    let url = `${this.baseUrl}/reports/monthly-reservations`;

    return this.httpClient.get<DataTableResponse<ReservationInfoResponse>>(url);
  }

  getYearlyReservations(): Observable<
    DataTableResponse<ReservationInfoResponse>
  > {
    let url = `${this.baseUrl}/reports/yearly-reservations`;

    return this.httpClient.get<DataTableResponse<ReservationInfoResponse>>(url);
  }

  getDailyReservations(): Observable<
    DataTableResponse<ReservationInfoResponse>
  > {
    let url = `${this.baseUrl}/reports/daily-reservations`;

    return this.httpClient.get<DataTableResponse<ReservationInfoResponse>>(url);
  }

  getWeeklyReservations(): Observable<
    DataTableResponse<ReservationInfoResponse>
  > {
    let url = `${this.baseUrl}/reports/weekly-reservations`;

    return this.httpClient.get<DataTableResponse<ReservationInfoResponse>>(url);
  }
}
