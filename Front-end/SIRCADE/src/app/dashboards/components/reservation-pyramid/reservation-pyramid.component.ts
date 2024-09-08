import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { DashboardsService } from '../../services/dashboards.service';
import {
  ApexAxisChartSeries,
  ApexChart,
  ApexDataLabels,
  ApexLegend,
  ApexPlotOptions,
  ApexTitleSubtitle,
  ApexTooltip,
  ApexXAxis,
  NgApexchartsModule,
} from 'ng-apexcharts';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MaterialModule } from 'src/app/material.module';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  xaxis: ApexXAxis;
  colors: string[];
  legend: ApexLegend;
  title: ApexTitleSubtitle;
  tooltip: ApexTooltip;
};

@Component({
  selector: 'app-reservation-pyramid',
  standalone: true,
  imports: [NgApexchartsModule, MaterialModule],
  templateUrl: './reservation-pyramid.component.html',
  styleUrl: './reservation-pyramid.component.scss',
})
export class ReservationPyramidComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  dashboardsService = inject(DashboardsService);

  data: number[];
  labels: string[];
  loading: boolean = false;
  chartOptions: Partial<ChartOptions>;

  ngOnInit(): void {
    this.loading = true;

    this.dashboardsService
      .getReservations()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        response = response.sort((a, b) => b.quantity - a.quantity);
        let totalItems = 0;
        response = response.map((item, index) => {
          totalItems += 1;
          return {
            label: item.label + ': ' + item.quantity,
            quantity: totalItems,
          };
        });
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
          name: 'reservation rank',
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
        zoom: {
          enabled: false,
        },
      },
      dataLabels: {
        enabled: true,
        formatter: function (val, opts) {
          return opts.w.globals.labels[opts.dataPointIndex];
        },
        dropShadow: {
          enabled: true,
        },
        style: {
          fontSize: '18px',
        },
      },
      plotOptions: {
        bar: {
          borderRadius: 0,
          horizontal: true,
          distributed: true,
          barHeight: '80%',
          isFunnel: true,
        },
      },
      xaxis: {
        categories: this.labels,
        labels: {
          style: {
            fontSize: '20px',
          },
        },
      },
      legend: {
        show: false,
      },
      tooltip: {
        enabled: false,
      },
    };
  }
}
