import { Component, inject } from '@angular/core';
import { TablerIconsModule } from 'angular-tabler-icons';
import { AppCustomersComponent } from 'src/app/components/dashboard1/customers/customers.component';
import { AppEmployeeSalaryComponent } from 'src/app/components/dashboard1/employee-salary/employee-salary.component';
import { AppMonthlyEarningsComponent } from 'src/app/components/dashboard1/monthly-earnings/monthly-earnings.component';
import { AppProjectsComponent } from 'src/app/components/dashboard1/projects/projects.component';
import { AppRevenueUpdatesComponent } from 'src/app/components/dashboard1/revenue-updates/revenue-updates.component';
import { AppSellingProductComponent } from 'src/app/components/dashboard1/selling-product/selling-product.component';
import { AppSocialCardComponent } from 'src/app/components/dashboard1/social-card/social-card.component';
import { AppTopCardsComponent } from 'src/app/components/dashboard1/top-cards/top-cards.component';
import { AppTopProjectsComponent } from 'src/app/components/dashboard1/top-projects/top-projects.component';
import { AppWeeklyStatsComponent } from 'src/app/components/dashboard1/weekly-stats/weekly-stats.component';
import { AppYearlyBreakupComponent } from 'src/app/components/dashboard1/yearly-breakup/yearly-breakup.component';
import { AppProductsComponent } from 'src/app/components/dashboard2/products/products.component';
import { ReservationWidgetsComponent } from '../../components/reservation-widgets/reservation-widgets.component';
import { SportFieldReservationsComponent } from '../../components/sport-field-reservations/sport-field-reservations.component';
import { SportFieldRescheduledReservationsComponent } from '../../components/sport-field-rescheduled-reservations/sport-field-rescheduled-reservations.component';
import { SportFieldCancelledReservationsComponent } from '../../components/sport-field-cancelled-reservations/sport-field-cancelled-reservations.component';
import { GradesReservationsComponent } from '../../components/grades-reservations/grades-reservations.component';
import { ReservationsByMonthComponent } from '../../components/reservations-by-month/reservations-by-month.component';
import { ReservationPyramidComponent } from '../../components/reservation-pyramid/reservation-pyramid.component';
import { AccountsService } from 'src/app/auth/services/accounts.service';

@Component({
  selector: 'app-main-dashboard',
  standalone: true,
  imports: [
    TablerIconsModule,
    AppTopCardsComponent,
    AppRevenueUpdatesComponent,
    AppYearlyBreakupComponent,
    AppMonthlyEarningsComponent,
    AppEmployeeSalaryComponent,
    AppCustomersComponent,
    AppProductsComponent,
    AppSocialCardComponent,
    AppSellingProductComponent,
    AppWeeklyStatsComponent,
    AppTopProjectsComponent,
    AppProjectsComponent,
    ReservationWidgetsComponent,
    SportFieldReservationsComponent,
    SportFieldRescheduledReservationsComponent,
    SportFieldCancelledReservationsComponent,
    GradesReservationsComponent,
    ReservationsByMonthComponent,
    ReservationPyramidComponent,
  ],
  templateUrl: './main-dashboard.component.html',
  styleUrl: './main-dashboard.component.scss',
})
export class MainDashboardComponent {
  accountsService = inject(AccountsService);
}
