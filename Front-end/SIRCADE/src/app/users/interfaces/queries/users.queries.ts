import { DataTableQueries } from 'src/app/shared/interfaces/queries/data-table.queries';

export interface UsersQueries extends DataTableQueries {
  roles: string[];
}
