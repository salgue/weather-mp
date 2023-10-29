import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DailyWeather } from 'src/app/models/daily-weather';
import { DailyWeatherUrlQuery } from 'src/app/models/daily-weather-url-query';
import { WeeklyWeather } from 'src/app/models/weekly-weather';
import { WeeklyWeatherUrlQuery } from 'src/app/models/weekly-weather-url-query';
import { WEATHER_ROUTES } from 'src/app/utils/consts';

@Injectable({
  providedIn: 'root',
})
export class WeatherSearchService {
  private _baseUrl: string = '';
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  getWeatherByCityorZipCode(
    weatherUrlQuery: WeeklyWeatherUrlQuery
  ): Observable<WeeklyWeather[]> {
    const url = `${this._baseUrl}${WEATHER_ROUTES.GetWeeklyRoute}`;
    return this.http.post<WeeklyWeather[]>(url, weatherUrlQuery);
  }

  getDailyWeather(weatherUrlQuery: DailyWeatherUrlQuery): Observable<DailyWeather> {
    const url = `${this._baseUrl}${WEATHER_ROUTES.GetDailyRoute}`;
    return this.http.post<DailyWeather>(url, weatherUrlQuery);
  }
}
