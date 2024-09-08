import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {
  ApexAxisChartSeries,
  ApexChart,
  ApexDataLabels,
  ApexGrid,
  ApexLegend,
  ApexPlotOptions,
  ApexTooltip,
  ApexXAxis,
  ApexYAxis,
  NgApexchartsModule,
} from 'ng-apexcharts';
import { DashboardsService } from '../../services/dashboards.service';
import { ScheduleProgrammingState } from 'src/app/schedules_programming/interfaces/enums/schedule-programming-state.enum';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MaterialModule } from 'src/app/material.module';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  grid: ApexGrid;
  colors: string[];
  legend: ApexLegend;
  tooltip: ApexTooltip;
};

@Component({
  selector: 'app-sport-field-reservations',
  standalone: true,
  imports: [NgApexchartsModule, MaterialModule],
  templateUrl: './sport-field-reservations.component.html',
  styleUrl: './sport-field-reservations.component.scss',
})
export class SportFieldReservationsComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  dashboardsService = inject(DashboardsService);

  data: number[];
  labels: string[];
  loading: boolean = false;
  chartOptions: Partial<ChartOptions>;

  constructor() {}
  ngOnInit(): void {
    this.loading = true;

    this.dashboardsService
      .getReservations()
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
          name: '# Reservas',
          data: this.data,
        },
      ],
      chart: {
        height: 350,
        type: 'bar',
        foreColor: '#adb0bb',
        toolbar: {
          show: false,
        },
      },
      plotOptions: {
        bar: {
          borderRadius: 4,
          columnWidth: '45%',
          distributed: true,
        },
      },
      dataLabels: {
        enabled: false,
      },
      legend: {
        show: false,
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
        theme: 'dark',
        fillSeriesColor: false,
      },
    };
  }
}
