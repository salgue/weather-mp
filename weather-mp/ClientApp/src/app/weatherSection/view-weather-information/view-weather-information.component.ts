import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DailyWeather } from 'src/app/models/daily-weather';
import { DailyWeatherUrlQuery } from 'src/app/models/daily-weather-url-query';
import { HourlyWeather } from 'src/app/models/hourly-weather';
import { WeatherSearchService } from 'src/app/services/weather/weather-search.service';

@Component({
  selector: 'app-view-weather-information',
  templateUrl: './view-weather-information.component.html',
  styleUrls: ['./view-weather-information.component.css'],
})
export class ViewWeatherInformationComponent {
  dataSource: HourlyWeather[] = [];
  date: string = '';
  constructor(
    @Inject(MAT_DIALOG_DATA) public dailyWeatherUrlQuery: DailyWeatherUrlQuery,
    private WeatherSearchService: WeatherSearchService
  ) {
    this.getDailyWeather();
  }

  getDailyWeather() {
    this.WeatherSearchService.getDailyWeather(
      this.dailyWeatherUrlQuery
    ).subscribe({
      next: (response: DailyWeather) => {
        console.log(response);
        this.dataSource = response.hourlyForecast;
        this.date = response.day;
      },
    });
  }

  get displayedColumns() {
    return ['hour','temperature','maxTemp','minTemp','description','icon']
  }
}
