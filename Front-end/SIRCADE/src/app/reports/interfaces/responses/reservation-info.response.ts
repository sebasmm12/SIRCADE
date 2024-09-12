import { TypeQuantitiesDto } from '../dtos/type-quantities';

export interface ReservationInfoResponse {
  label: string;
  typeQuantities: TypeQuantitiesDto[];
}
