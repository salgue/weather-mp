import { Component, OnDestroy } from '@angular/core';
import { DialogService } from 'src/app/services/dialog/dialog.service';
import { WeatherDataService } from 'src/app/services/weather/weather-data.service';
import { ViewWeatherInformationComponent } from '../view-weather-information/view-weather-information.component';
import { DailyWeatherUrlQuery } from 'src/app/models/daily-weather-url-query';

@Component({
  selector: 'app-weathers-list',
  templateUrl: './weathers-list.component.html',
  styleUrls: ['./weathers-list.component.css'],
})
export class WeathersListComponent implements OnDestroy {
  constructor(
    private weatherDataService: WeatherDataService,
    private dialogService: DialogService,
  ) { }
  
  ngOnDestroy(): void {
    this.weatherDataService.resetValues();
  }

  openWeatherModal(searchDate: string) {
    const searchValue = this.weatherDataService.searchValue;
    const dataQuery = <DailyWeatherUrlQuery>{
      searchDate,
      searchValue
    }
    this.dialogService.openDialog(ViewWeatherInformationComponent, dataQuery);
  }

  get dailyForecast(): any[] {
    return this.weatherDataService.dailyForecast;
  }
}
