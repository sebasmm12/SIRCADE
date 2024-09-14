import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FrequentlyUsersQueries } from '../interfaces/queries/frequently-users.queries';
import queryString from 'query-string';
import { DataTableResponse } from '../../shared/interfaces/responses/data-table.response';
import { Observable } from 'rxjs';
import { ReservationInfoResponse } from '../interfaces/responses/reservation-info.response';
import { FrequentlyUsersExportQueries } from '../interfaces/queries/frequently-users-export.queries';

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

  exportUsers(
    frequentlyUsersExportQueries: FrequentlyUsersExportQueries
  ): Observable<string> {
    const params = queryString.stringify(frequentlyUsersExportQueries);

    let url = `${this.baseUrl}/reports/frequently-users/exports?${params}`;

    return this.httpClient.get<string>(url, { responseType: 'text' as 'json' });
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

  exportMonthlyReservations(reportTitle: string): Observable<string> {
    let url = `${this.baseUrl}/reports/monthly-reservations/exports?reportTitle=${reportTitle}`;

    return this.httpClient.get<string>(url, { responseType: 'text' as 'json' });
  }

  getYearlyReservations(): Observable<
    DataTableResponse<ReservationInfoResponse>
  > {
    let url = `${this.baseUrl}/reports/yearly-reservations`;

    return this.httpClient.get<DataTableResponse<ReservationInfoResponse>>(url);
  }

  exportYearlyReservations(reportTitle: string): Observable<string> {
    let url = `${this.baseUrl}/reports/yearly-reservations/exports?reportTitle=${reportTitle}`;

    return this.httpClient.get<string>(url, { responseType: 'text' as 'json' });
  }

  getDailyReservations(): Observable<
    DataTableResponse<ReservationInfoResponse>
  > {
    let url = `${this.baseUrl}/reports/daily-reservations`;

    return this.httpClient.get<DataTableResponse<ReservationInfoResponse>>(url);
  }

  exportDailyReservations(reportTitle: string): Observable<string> {
    let url = `${this.baseUrl}/reports/daily-reservations/exports?reportTitle=${reportTitle}`;

    return this.httpClient.get<string>(url, { responseType: 'text' as 'json' });
  }

  getWeeklyReservations(): Observable<
    DataTableResponse<ReservationInfoResponse>
  > {
    let url = `${this.baseUrl}/reports/weekly-reservations`;

    return this.httpClient.get<DataTableResponse<ReservationInfoResponse>>(url);
  }

  exportWeeklyReservations(reportTitle: string): Observable<string> {
    let url = `${this.baseUrl}/reports/weekly-reservations/exports?reportTitle=${reportTitle}`;

    return this.httpClient.get<string>(url, { responseType: 'text' as 'json' });
  }

  getSportFieldTypesByTurn(): Observable<
    DataTableResponse<ReservationInfoResponse>
  > {
    let url = `${this.baseUrl}/reports/sport-fields-by-turn`;

    return this.httpClient.get<DataTableResponse<ReservationInfoResponse>>(url);
  }

  exportSportFieldTypesByTurn(reportTitle: string): Observable<string> {
    let url = `${this.baseUrl}/reports/sport-fields-by-turn/exports?reportTitle=${reportTitle}`;

    return this.httpClient.get<string>(url, { responseType: 'text' as 'json' });
  }
}
