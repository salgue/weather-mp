import { Component } from '@angular/core';
import { DashboardDataService } from '../services/dashboard/dashboard-data.service';
import { FavoritesService } from '../services/favorites/favorites.service';
import { Dashboard } from '../models/dashboard';

@Component({
  selector: 'app-dashboards',
  templateUrl: './dashboards.component.html',
  styleUrls: ['./dashboards.component.css'],
})
export class DashboardsComponent {
  constructor(
    private dashboardDataService: DashboardDataService,
    private favoritesService: FavoritesService
  ) {
    this.getDashboardData();
  }

  getDashboardData() {
    if (this.favoritesService.verifyFavoriteCookie()) {
      const favoriteString = this.favoritesService.getFavoriteListFromCookie();

      const searchData = favoriteString.join(',');
      this.dashboardDataService.getDashboardData(searchData);
    }
  }

  removeFavorite(dashboardSelected: Dashboard) {
    this.dashboardDataService.removeCityFromFavoriteList(dashboardSelected);
    this.favoritesService.removeAsFavorite(dashboardSelected.city);
  }

  get dashboardList(): Dashboard[] {
    return this.dashboardDataService.dashboardList;
  }
}
