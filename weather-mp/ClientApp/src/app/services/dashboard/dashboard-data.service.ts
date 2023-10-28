import { Injectable } from '@angular/core';
import { DashboardService } from './dashboard.service';
import { DashboardUrlQuery } from '../../models/dashboard-url-query';
import { Dashboard } from 'src/app/models/dashboard';

@Injectable({
  providedIn: 'root'
})
export class DashboardDataService {

  private _dashboardList: Dashboard[] = [];
  constructor(private dashboardService: DashboardService) { }

  getDashboardData(searchData: string) {
    const dashboardUrlQuery: DashboardUrlQuery = {
      searchData
    }

    this.dashboardService.getDashboard(dashboardUrlQuery).subscribe({
      next: (response: Dashboard[]) => {
        console.log(response);
        this._dashboardList = response;
      },
    });
  }

  get dashboardList(): Dashboard[] { 
    return this._dashboardList;
  }
}
