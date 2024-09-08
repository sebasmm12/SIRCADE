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
  selector: 'app-grades-reservations',
  standalone: true,
  imports: [NgApexchartsModule, MaterialModule],
  templateUrl: './grades-reservations.component.html',
  styleUrl: './grades-reservations.component.scss',
})
export class GradesReservationsComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  dashboardsService = inject(DashboardsService);

  data: number[];
  labels: string[];
  loading: boolean = false;
  chartOptions: Partial<ChartOptions>;

  ngOnInit(): void {
    this.loading = true;

    this.dashboardsService
      .getReservationsByGrade()
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
          name: 're-scheduled reservations',
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
