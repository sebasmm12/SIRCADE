export interface ScheduleProgrammingInfoResponse {
  id: number;
  sportFieldId: number;
  sportFieldName: string;
  clientId?: number;
  clientName: string;
  type: number;
  typeName: string;
  startDate: Date;
  endDate: Date;
  comment?: string;
  registerUser: string;
  lightColor: string;
  darkColor: string;
}
