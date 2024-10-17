import { Component } from '@angular/core';
import { AppointmentChartComponent } from '../appointment-chart/appointment-chart.component';
import { PieChartBranchComponent } from '../pie-chart-brach/pie-chart-branch.component';
import { PaiChartTitleComponent } from '../pai-chart-title/pai-chart-title.component';
import { TotalNumberCardsComponent } from '../total-number-cards/total-number-cards.component';
import { AdminSidebarComponent } from '../../sidebar/adminSidebar.component';
import { TokenComponent } from '../../../../../../shared/components/token/token.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
  imports: [
    AppointmentChartComponent,
    PieChartBranchComponent,
    PaiChartTitleComponent,
    TotalNumberCardsComponent,
    AdminSidebarComponent,
    TokenComponent
  ],
})
export class DashboardComponent {}
