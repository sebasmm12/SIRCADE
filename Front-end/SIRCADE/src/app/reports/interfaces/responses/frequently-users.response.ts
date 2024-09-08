import { TypeQuantitiesDto } from '../dtos/type-quantities';

export interface FrequentlyUsersResponse {
  userName: string;
  sportFieldTypeQuantities: TypeQuantitiesDto[];
}
