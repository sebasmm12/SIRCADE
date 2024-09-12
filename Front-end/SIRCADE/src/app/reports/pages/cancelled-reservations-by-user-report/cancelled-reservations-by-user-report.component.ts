import {
  AfterViewInit,
  Component,
  DestroyRef,
  inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MaterialModule } from 'src/app/material.module';
import { SearchTextComponent } from 'src/app/shared/components/search-text/search-text.component';
import { ReportsService } from '../../services/reports.service';
import { TypeQuantitiesDto } from '../../interfaces/dtos/type-quantities';
import { MatPaginator } from '@angular/material/paginator';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { switchMap } from 'rxjs';
import { FrequentlyUsersQueries } from '../../interfaces/queries/frequently-users.queries';
import { ScheduleProgrammingState } from 'src/app/schedules_programming/interfaces/enums/schedule-programming-state.enum';
import { ReservationInfoResponse } from '../../interfaces/responses/reservation-info.response';

@Component({
  selector: 'app-cancelled-reservations-by-user-report',
  standalone: true,
  imports: [MaterialModule, SearchTextComponent],
  templateUrl: './cancelled-reservations-by-user-report.component.html',
  styleUrl: './cancelled-reservations-by-user-report.component.scss',
})
export class CancelledReservationsByUserReportComponent
  implements OnInit, AfterViewInit
{
  destroyRef = inject(DestroyRef);
  reportsService = inject(ReportsService);

  frequentlyUsers: ReservationInfoResponse[] = [];
  totalFrequentlyUsers: number = 0;
  frequentlyUserColumns: string[] = ['label'];
  pageSize: number = 10;
  searchText: string = '';
  loading: boolean = true;
  roles: string[] = ['Socio'];
  canIncludeColumns = false;
  sportFieldTypeColumns: string[] = [];
  sportFieldTypeQuantities: TypeQuantitiesDto[] = [];

  @ViewChild(MatPaginator)
  paginator: MatPaginator = Object.create(null);

  getQuantityByType(
    frequentlyUsers: ReservationInfoResponse,
    sportFieldType: string
  ): number {
    const quantity = frequentlyUsers.typeQuantities.find(
      (sportFieldTypeQuantity) => sportFieldTypeQuantity.name === sportFieldType
    )?.quantity;
    return quantity ?? 0;
  }

  ngOnInit(): void {
    this.canIncludeColumns = true;
    this.get();
  }
  ngAfterViewInit(): void {
    this.paginator.page
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        switchMap(() => {
          this.loading = true;
          const frequentlyUsersQueries: FrequentlyUsersQueries = {
            page: this.paginator.pageIndex,
            pageSize: this.paginator.pageSize,
            search: this.searchText,
            roles: this.roles,
            reservationStates: [ScheduleProgrammingState.Cancelled],
          };
          return this.reportsService.getFrequentlyUsers(frequentlyUsersQueries);
        })
      )
      .subscribe((response) => {
        this.frequentlyUsers = response.data;
        this.totalFrequentlyUsers = response.totalElements;
        this.loading = false;
      });
  }

  search(searchText: string): void {
    this.searchText = searchText;
    this.get();
  }

  get(): void {
    this.loading = true;

    this.reportsService
      .getFrequentlyUsers({
        page: 0,
        pageSize: this.pageSize,
        search: this.searchText,
        roles: this.roles,
        reservationStates: [ScheduleProgrammingState.Cancelled],
      })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        this.frequentlyUsers = response.data;
        this.totalFrequentlyUsers = response.totalElements;
        this.paginator.firstPage();

        if (this.canIncludeColumns) {
          this.sportFieldTypeQuantities =
            this.frequentlyUsers[0].typeQuantities;
          this.sportFieldTypeColumns =
            this.frequentlyUsers[0].typeQuantities.map(
              (sportField) => sportField.name
            );

          this.frequentlyUserColumns = ['label', ...this.sportFieldTypeColumns];

          this.canIncludeColumns = false;
        }

        this.loading = false;
      });
  }
}
