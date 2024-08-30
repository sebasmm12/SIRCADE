export interface ScheduleProgrammingRequest {
  sportFieldId: number;
  startDate: Date;
  endDate: Date;
  clientId?: number | null;
  type: number;
  comment: string;
}
