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
  selector: 'app-yearly-reservations',
  standalone: true,
  imports: [MaterialModule],
  templateUrl: './yearly-reservations.component.html',
  styleUrl: './yearly-reservations.component.scss',
})
export class YearlyReservationsComponent {
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
          return this.reportsService.getYearlyReservations();
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
      .getYearlyReservations()
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
    const reportTitle = 'Reservas anuales';

    const exportFunction = () =>
      this.reportsService
        .exportYearlyReservations(reportTitle)
        .pipe(takeUntilDestroyed(this.destroyRef));

    this.exportsService.downloadExcel(exportFunction, reportTitle);
  }
}
