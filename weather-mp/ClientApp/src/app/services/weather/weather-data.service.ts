import { Injectable } from '@angular/core';
import { WeeklyWeatherUrlQuery } from 'src/app/models/weekly-weather-url-query';
import { WeatherSearchService } from './weather-search.service';
import { WeeklyWeather } from 'src/app/models/weekly-weather';

@Injectable({
  providedIn: 'root',
})
export class WeatherDataService {
  private _dailyForecast: WeeklyWeather[] = [];
  constructor(private weatherSearchService: WeatherSearchService) {}

  getWeatherData(weatherUrlQuery: WeeklyWeatherUrlQuery) {
    this.weatherSearchService
      .getWeatherByCityorZipCode(weatherUrlQuery)
      .subscribe({
        next: (response: WeeklyWeather[]) => {
          this._dailyForecast = response;
        },
      });
  }

  get dailyForecast(): WeeklyWeather[] {
    return this._dailyForecast;
  }

}
