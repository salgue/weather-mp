import { Component, Input } from '@angular/core';
import { DailyWeatherUrlQuery } from 'src/app/models/daily-weather-url-query';
import { WeeklyWeather } from 'src/app/models/weekly-weather';
import { DialogService } from 'src/app/services/dialog/dialog.service';
import { WeatherDataService } from 'src/app/services/weather/weather-data.service';
import { ViewWeatherInformationComponent } from 'src/app/weatherSection/view-weather-information/view-weather-information.component';

@Component({
  selector: 'app-weather-card',
  templateUrl: './weather-card.component.html',
  styleUrls: ['./weather-card.component.css'],
})
export class WeatherCardComponent {
  @Input() weather!: WeeklyWeather;

  @Input() city: string = '';
  constructor(
    private weatherDataService: WeatherDataService,
    private dialogService: DialogService
  ) {}
  openWeatherModal(searchDate: string) {
    const searchValue =
      this.city === '' ? this.weatherDataService.searchValue : this.city;
    const dataQuery = <DailyWeatherUrlQuery>{
      searchDate,
      searchValue,
    };
    this.dialogService.openDialog(ViewWeatherInformationComponent, dataQuery);
  }

  get dailyForecast(): any[] {
    return this.weatherDataService.dailyForecast;
  }
}
