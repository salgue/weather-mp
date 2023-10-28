import { Injectable } from '@angular/core';
import { WeeklyWeatherUrlQuery } from 'src/app/models/weekly-weather-url-query';
import { WeatherSearchService } from './weather-search.service';
import { WeeklyWeather } from 'src/app/models/weekly-weather';

@Injectable({
  providedIn: 'root',
})
export class WeatherDataService {
  private _dailyForecast: WeeklyWeather[] = [];
  
  searchValue: string = '';
  constructor(private weatherSearchService: WeatherSearchService) {}

  getWeatherData(weatherUrlQuery: WeeklyWeatherUrlQuery) {
    this.weatherSearchService
      .getWeatherByCityorZipCode(weatherUrlQuery)
      .subscribe({
        next: (response: WeeklyWeather[]) => {
          console.log(response);
          this._dailyForecast = response;
          console.log(this._dailyForecast);
        },
      });
  }

  get dailyForecast(): WeeklyWeather[] {
    return this._dailyForecast;
  }

}
