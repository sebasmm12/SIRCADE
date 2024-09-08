import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FrequentlyUsersQueries } from '../interfaces/queries/frequently-users.queries';
import queryString from 'query-string';
import { DataTableResponse } from '../../shared/interfaces/responses/data-table.response';
import { FrequentlyUsersResponse } from '../interfaces/responses/frequently-users.response';
import { Observable } from 'rxjs';
import { MonthlyReservationsComponent } from '../pages/monthly-reservations/monthly-reservations.component';
import { ReservationsByDateResponse } from '../interfaces/responses/reservations-by-date.response';

@Injectable({
  providedIn: 'root',
})
export class ReportsService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  getFrequentlyUsers(
    frequentlyUsersQueries: FrequentlyUsersQueries
  ): Observable<DataTableResponse<FrequentlyUsersResponse>> {
    const params = queryString.stringify(frequentlyUsersQueries);

    let url = `${this.baseUrl}/reports/frequently-users?${params}`;

    return this.httpClient.get<DataTableResponse<FrequentlyUsersResponse>>(url);
  }

  getCancelledReservationsByUser(
    frequentlyUsersQueries: FrequentlyUsersQueries
  ) {
    const params = queryString.stringify(frequentlyUsersQueries);

    let url = `${this.baseUrl}/reports/cancelled-reservations-by-user?${params}`;

    return this.httpClient.get(url);
  }

  getMonthlyReservations(): Observable<
    DataTableResponse<ReservationsByDateResponse>
  > {
    let url = `${this.baseUrl}/reports/monthly-reservations`;

    return this.httpClient.get<DataTableResponse<ReservationsByDateResponse>>(
      url
    );
  }

  getYearlyReservations(): Observable<
    DataTableResponse<ReservationsByDateResponse>
  > {
    let url = `${this.baseUrl}/reports/yearly-reservations`;

    return this.httpClient.get<DataTableResponse<ReservationsByDateResponse>>(
      url
    );
  }

  getDailyReservations(): Observable<
    DataTableResponse<ReservationsByDateResponse>
  > {
    let url = `${this.baseUrl}/reports/daily-reservations`;

    return this.httpClient.get<DataTableResponse<ReservationsByDateResponse>>(
      url
    );
  }

  getWeeklyReservations(): Observable<
    DataTableResponse<ReservationsByDateResponse>
  > {
    let url = `${this.baseUrl}/reports/weekly-reservations`;

    return this.httpClient.get<DataTableResponse<ReservationsByDateResponse>>(
      url
    );
  }
}
