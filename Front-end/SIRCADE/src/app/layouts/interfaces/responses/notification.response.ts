import { NotificationStatus } from '../enums/notification-status.enum';

export interface NotificationResponse {
  id: number;
  subject: string;
  message: string;
  status: NotificationStatus;
  deliveringDate: Date;
}
