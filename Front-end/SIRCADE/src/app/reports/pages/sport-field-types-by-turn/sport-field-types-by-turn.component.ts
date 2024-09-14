import { Component, DestroyRef, inject, ViewChild } from '@angular/core';
import { ReportsService } from '../../services/reports.service';
import { ReservationInfoResponse } from '../../interfaces/responses/reservation-info.response';
import { TypeQuantitiesDto } from '../../interfaces/dtos/type-quantities';
import { MatPaginator } from '@angular/material/paginator';
import { ExportsService } from '../../services/exports.service';
import { MaterialModule } from 'src/app/material.module';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { switchMap } from 'rxjs';

@Component({
  selector: 'app-sport-field-types-by-turn',
  standalone: true,
  imports: [MaterialModule],
  templateUrl: './sport-field-types-by-turn.component.html',
  styleUrl: './sport-field-types-by-turn.component.scss',
})
export class SportFieldTypesByTurnComponent {
  destroyRef = inject(DestroyRef);
  reportsService = inject(ReportsService);
  exportsService = inject(ExportsService);

  sportFieldTypesByTurns: ReservationInfoResponse[] = [];
  totalSportFieldTypesByTurns: number = 0;
  sportFieldTypesByTurnsColumns: string[] = ['label'];
  pageSize: number = 10;
  searchText: string = '';
  loading: boolean = true;
  canIncludeColumns = false;
  sportFieldTypeColumns: string[] = [];
  sportFieldTypeQuantities: TypeQuantitiesDto[] = [];

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
          return this.reportsService.getSportFieldTypesByTurn();
        })
      )
      .subscribe((response) => {
        this.sportFieldTypesByTurns = response.data;
        this.totalSportFieldTypesByTurns = response.totalElements;
        this.loading = false;
      });
  }

  get(): void {
    this.loading = true;

    this.reportsService
      .getSportFieldTypesByTurn()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        this.sportFieldTypesByTurns = response.data;
        this.totalSportFieldTypesByTurns = response.totalElements;
        this.paginator.firstPage();

        if (this.canIncludeColumns) {
          this.sportFieldTypeQuantities =
            this.sportFieldTypesByTurns[0].typeQuantities;
          this.sportFieldTypeColumns =
            this.sportFieldTypesByTurns[0].typeQuantities.map(
              (dateType) => dateType.name
            );

          this.sportFieldTypesByTurnsColumns = [
            'label',
            ...this.sportFieldTypeColumns,
          ];

          this.canIncludeColumns = false;
        }

        this.loading = false;
      });
  }

  export(): void {
    const reportTitle = 'Canchas deportivas por turno';

    const exportFunction = () =>
      this.reportsService
        .exportSportFieldTypesByTurn(reportTitle)
        .pipe(takeUntilDestroyed(this.destroyRef));

    this.exportsService.downloadExcel(exportFunction, reportTitle);
  }
}
