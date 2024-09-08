import { Component, DestroyRef, inject, ViewChild } from '@angular/core';
import { MaterialModule } from 'src/app/material.module';
import { ReportsService } from '../../services/reports.service';
import { ReservationsByDateResponse } from '../../interfaces/responses/reservations-by-date.response';
import { TypeQuantitiesDto } from '../../interfaces/dtos/type-quantities';
import { MatPaginator } from '@angular/material/paginator';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { switchMap } from 'rxjs';

@Component({
  selector: 'app-daily-reservations',
  standalone: true,
  imports: [MaterialModule],
  templateUrl: './daily-reservations.component.html',
  styleUrl: './daily-reservations.component.scss',
})
export class DailyReservationsComponent {
  destroyRef = inject(DestroyRef);
  reportsService = inject(ReportsService);

  monthlyReservations: ReservationsByDateResponse[] = [];
  totalMonthlyReservations: number = 0;
  monthlyReservationColumns: string[] = ['state'];
  pageSize: number = 10;
  searchText: string = '';
  loading: boolean = true;
  canIncludeColumns = false;
  monthTypeColumns: string[] = [];
  monthTypeQuantities: TypeQuantitiesDto[] = [];

  @ViewChild(MatPaginator)
  paginator: MatPaginator = Object.create(null);

  getQuantityByType(
    dailyReservation: ReservationsByDateResponse,
    dailyType: string
  ): number {
    const quantity = dailyReservation.typeQuantities.find(
      (monthTypeQuantity) => monthTypeQuantity.name === dailyType
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
          return this.reportsService.getDailyReservations();
        })
      )
      .subscribe((response) => {
        this.monthlyReservations = response.data;
        this.totalMonthlyReservations = response.totalElements;
        this.loading = false;
      });
  }

  get(): void {
    this.loading = true;

    this.reportsService
      .getDailyReservations()
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

          this.monthlyReservationColumns = ['state', ...this.monthTypeColumns];

          this.canIncludeColumns = false;
        }

        this.loading = false;
      });
  }
}
