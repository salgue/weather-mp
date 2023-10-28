import { Injectable } from '@angular/core';
import { WeeklyWeatherUrlQuery } from 'src/app/models/weekly-weather-url-query';
import { WeatherSearchService } from './weather-search.service';
import { WeeklyWeather } from 'src/app/models/weekly-weather';
import { FavoritesService } from '../favorites/favorites.service';
import { SpinnerService } from '../spinner/spinner.service';

@Injectable({
  providedIn: 'root',
})
export class WeatherDataService {
  private _dailyForecast: WeeklyWeather[] = [];
  
  searchValue: string = '';
  constructor(private weatherSearchService: WeatherSearchService,
    private favoritesService: FavoritesService, private spinnerService: SpinnerService) {}

  resetValues() {
    this._dailyForecast = [];
    this.searchValue = '';
  }
  
  getWeatherData(weatherUrlQuery: WeeklyWeatherUrlQuery, favorite: boolean = false) {
    this.spinnerService.startSpinner();
    this.weatherSearchService
      .getWeatherByCityorZipCode(weatherUrlQuery)
      .subscribe({
        next: (response: WeeklyWeather[]) => {
          console.log(response);
          if (response) {
            this._dailyForecast = response;
            if (favorite) {
              this.favoritesService.addFavoriteListToCookie(
                favorite,
                weatherUrlQuery.searchValue
              );  
            }
          }
          this.spinnerService.closeSpinner();
        },
      });
  }

  get dailyForecast(): WeeklyWeather[] {
    return this._dailyForecast;
  }

}
