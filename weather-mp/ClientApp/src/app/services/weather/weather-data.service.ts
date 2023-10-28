import { Injectable } from '@angular/core';
import { WeeklyWeatherUrlQuery } from 'src/app/models/weekly-weather-url-query';
import { WeatherSearchService } from './weather-search.service';
import { WeeklyWeather } from 'src/app/models/weekly-weather';
import { FavoritesService } from '../favorites/favorites.service';

@Injectable({
  providedIn: 'root',
})
export class WeatherDataService {
  private _dailyForecast: WeeklyWeather[] = [];
  
  searchValue: string = '';
  constructor(private weatherSearchService: WeatherSearchService,
    private favoritesService: FavoritesService) {}

  resetValues() {
    this._dailyForecast = [];
    this.searchValue = '';
  }
  
  getWeatherData(weatherUrlQuery: WeeklyWeatherUrlQuery, favorite:boolean = false) {
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
        },
      });
  }

  get dailyForecast(): WeeklyWeather[] {
    return this._dailyForecast;
  }

}
