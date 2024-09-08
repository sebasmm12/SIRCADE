import { ScheduleProgrammingState } from 'src/app/schedules_programming/interfaces/enums/schedule-programming-state.enum';
import { DataTableQueries } from 'src/app/shared/interfaces/queries/data-table.queries';

export interface FrequentlyUsersQueries extends DataTableQueries {
  roles: string[];
  reservationStates: ScheduleProgrammingState[];
}
