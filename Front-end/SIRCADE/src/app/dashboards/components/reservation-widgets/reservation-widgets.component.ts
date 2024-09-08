import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { MaterialModule } from 'src/app/material.module';
import { DashboardsService } from '../../services/dashboards.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { DashboardWidgetsResponse } from '../../interfaces/responses/dashboard-widgets.response';

@Component({
  selector: 'app-reservation-widgets',
  standalone: true,
  imports: [MaterialModule],
  templateUrl: './reservation-widgets.component.html',
  styleUrl: './reservation-widgets.component.scss',
})
export class ReservationWidgetsComponent implements OnInit {
  destroyRef = inject(DestroyRef);

  dashboardsService = inject(DashboardsService);

  dashboardWidgets!: DashboardWidgetsResponse;

  loading: boolean = false;

  constructor() {}

  ngOnInit(): void {
    this.loading = true;

    this.dashboardsService
      .getWidgets()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((response) => {
        this.dashboardWidgets = response;
        this.loading = false;
      });
  }
}
