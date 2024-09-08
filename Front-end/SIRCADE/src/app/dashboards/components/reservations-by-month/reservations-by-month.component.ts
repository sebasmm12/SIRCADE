import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import {
  ApexAxisChartSeries,
  ApexChart,
  ApexDataLabels,
  ApexGrid,
  ApexStroke,
  ApexTitleSubtitle,
  ApexTooltip,
  ApexXAxis,
  NgApexchartsModule,
} from 'ng-apexcharts';
import { DashboardsService } from '../../services/dashboards.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MaterialModule } from 'src/app/material.module';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  dataLabels: ApexDataLabels;
  grid: ApexGrid;
  stroke: ApexStroke;
  title: ApexTitleSubtitle;
  tooltip: ApexTooltip;
};

@Component({
  selector: 'app-reservations-by-month',
  standalone: true,
  imports: [NgApexchartsModule, MaterialModule],
  templateUrl: './reservations-by-month.component.html',
  styleUrl: './reservations-by-month.component.scss',
})
export class ReservationsByMonthComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  dashboardsService = inject(DashboardsService);

  data: number[];
  labels: string[];
  loading: boolean = false;
  chartOptions: Partial<ChartOptions>;

  ngOnInit(): void {
    this.loading = true;

    this.dashboardsService
      .getReservationsByMonth()
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
      series: [
        {
          name: 'reservations by month',
          data: this.data,
        },
      ],
      chart: {
        height: 350,
        type: 'line',
        foreColor: '#adb0bb',
        toolbar: {
          show: false,
        },
        zoom: {
          enabled: false,
        },
      },
      dataLabels: {
        enabled: true,
      },
      stroke: {
        curve: 'straight',
      },
      grid: {
        show: false,
      },
      xaxis: {
        categories: this.labels,
        labels: {
          style: {
            fontSize: '12px',
          },
        },
      },
      tooltip: {
        enabled: false,
      },
    };
  }
}
