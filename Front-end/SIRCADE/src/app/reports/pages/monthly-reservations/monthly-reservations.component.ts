import { Component, DestroyRef, inject, ViewChild } from '@angular/core';
import { MaterialModule } from 'src/app/material.module';
import { SearchTextComponent } from 'src/app/shared/components/search-text/search-text.component';
import { ReportsService } from '../../services/reports.service';
import { TypeQuantitiesDto } from '../../interfaces/dtos/type-quantities';
import { MatPaginator } from '@angular/material/paginator';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { switchMap } from 'rxjs';
import { ReservationInfoResponse } from '../../interfaces/responses/reservation-info.response';

@Component({
  selector: 'app-monthly-reservations',
  standalone: true,
  imports: [MaterialModule, SearchTextComponent],
  templateUrl: './monthly-reservations.component.html',
  styleUrl: './monthly-reservations.component.scss',
})
export class MonthlyReservationsComponent {
  destroyRef = inject(DestroyRef);
  reportsService = inject(ReportsService);

  monthlyReservations: ReservationInfoResponse[] = [];
  totalMonthlyReservations: number = 0;
  monthlyReservationColumns: string[] = ['label'];
  pageSize: number = 10;
  searchText: string = '';
  loading: boolean = true;
  canIncludeColumns = false;
  monthTypeColumns: string[] = [];
  monthTypeQuantities: TypeQuantitiesDto[] = [];

  @ViewChild(MatPaginator)
  paginator: MatPaginator = Object.create(null);

  getQuantityByType(
    monthlyReservation: ReservationInfoResponse,
    monthType: string
  ): number {
    const quantity = monthlyReservation.typeQuantities.find(
      (monthTypeQuantity) => monthTypeQuantity.name === monthType
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
          return this.reportsService.getMonthlyReservations();
        })
      )
      .subscribe((response) => {
        this.monthlyReservations = response.data;
        this.totalMonthlyReservations = response.totalElements;
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
      .getMonthlyReservations()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        this.monthlyReservations = response.data;
        this.totalMonthlyReservations = response.totalElements;
        this.paginator.firstPage();

        if (this.canIncludeColumns) {
          this.monthTypeQuantities = this.monthlyReservations[0].typeQuantities;
          this.monthTypeColumns =
            this.monthlyReservations[0].typeQuantities.map(
              (dateType) => dateType.name
            );

          this.monthlyReservationColumns = ['label', ...this.monthTypeColumns];

          this.canIncludeColumns = false;
        }

        this.loading = false;
      });
  }
}
