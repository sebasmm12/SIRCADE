import { Component, DestroyRef, inject, ViewChild } from '@angular/core';
import { MaterialModule } from 'src/app/material.module';
import { ReportsService } from '../../services/reports.service';
import { TypeQuantitiesDto } from '../../interfaces/dtos/type-quantities';
import { MatPaginator } from '@angular/material/paginator';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { switchMap } from 'rxjs';
import { ReservationInfoResponse } from '../../interfaces/responses/reservation-info.response';
import { ExportsService } from '../../services/exports.service';

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
  exportsService = inject(ExportsService);

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
    dailyReservation: ReservationInfoResponse,
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

          this.monthlyReservationColumns = ['label', ...this.monthTypeColumns];

          this.canIncludeColumns = false;
        }

        this.loading = false;
      });
  }

  export(): void {
    const reportTitle = 'Reservas diarias';

    const exportFunction = () =>
      this.reportsService
        .exportDailyReservations(reportTitle)
        .pipe(takeUntilDestroyed(this.destroyRef));

    this.exportsService.downloadExcel(exportFunction, reportTitle);
  }
}
