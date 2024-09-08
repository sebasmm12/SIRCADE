import { TypeQuantitiesDto } from '../dtos/type-quantities';

export interface ReservationsByDateResponse {
  state: string;
  typeQuantities: TypeQuantitiesDto[];
}
