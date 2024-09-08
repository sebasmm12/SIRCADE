import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import {
  ApexChart,
  ApexNonAxisChartSeries,
  ApexResponsive,
  NgApexchartsModule,
} from 'ng-apexcharts';
import { MaterialModule } from 'src/app/material.module';
import { DashboardsService } from '../../services/dashboards.service';
import { ScheduleProgrammingState } from 'src/app/schedules_programming/interfaces/enums/schedule-programming-state.enum';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};

@Component({
  selector: 'app-sport-field-cancelled-reservations',
  standalone: true,
  imports: [NgApexchartsModule, MaterialModule],
  templateUrl: './sport-field-cancelled-reservations.component.html',
  styleUrl: './sport-field-cancelled-reservations.component.scss',
})
export class SportFieldCancelledReservationsComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  dashboardsService = inject(DashboardsService);

  data: number[];
  labels: string[];
  loading: boolean = false;
  chartOptions: Partial<ChartOptions>;

  ngOnInit(): void {
    this.loading = true;

    this.dashboardsService
      .getReservations(ScheduleProgrammingState.Cancelled)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        this.data = response.map((item) => item.quantity);
        this.labels = response.map((item) => item.label);
        this.setChartOptions();
        this.loading = false;
      });
  }

  setChartOptions(): void {
    this.chartOptions = {
      series: this.data,
      chart: {
        height: 350,
        type: 'pie',
        foreColor: '#adb0bb',
        toolbar: {
          show: false,
        },
        zoom: {
          enabled: false,
        },
      },
      labels: this.labels,
    };
  }
}
