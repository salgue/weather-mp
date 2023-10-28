import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Dashboard } from 'src/app/models/dashboard';
import { DashboardUrlQuery } from 'src/app/models/dashboard-url-query';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  private _baseUrl: string = '';
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  getDashboard(dashboardUrlQuery: DashboardUrlQuery): Observable<Dashboard[]> {
    const url = `${this._baseUrl}dashboard/GetDashboard`;
    return this.http.post<Dashboard[]>(url, dashboardUrlQuery);
  }
}
