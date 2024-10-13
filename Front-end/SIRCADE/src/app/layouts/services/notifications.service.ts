import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NotificationResponse } from '../interfaces/responses/notification.response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NotificationsService {
  private baseUrl: string = environment.apiUrl;

  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  getNotifications(): Observable<NotificationResponse[]> {
    return this.httpClient.get<NotificationResponse[]>(
      `${this.baseUrl}/notifications`
    );
  }
}
