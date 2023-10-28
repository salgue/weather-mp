import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { WeeklyWeatherUrlQuery } from 'src/app/models/weekly-weather-url-query';
import { WeatherDataService } from 'src/app/services/weather/weather-data.service';
import { WeatherFormFactoryService } from 'src/app/services/weather/weather-form-factory.service';

@Component({
  selector: 'app-weather-search',
  templateUrl: './weather-search.component.html',
  styleUrls: ['./weather-search.component.css'],
})
export class WeatherSearchComponent {
  constructor(
    private weatherFactoryService: WeatherFormFactoryService,
    private weatherDataService: WeatherDataService
  ) {
    weatherFactoryService.initializeSearchForm();
  }

  search() {
    if (!this.searchValueControl.value) {
      console.error('Value Required');
      return;
    }

    const weatherUrlQuery = <WeeklyWeatherUrlQuery>this.searchForm.value;

    this.weatherDataService.getWeatherData(weatherUrlQuery);
    this.weatherDataService.searchValue = weatherUrlQuery.searchValue;
  }

  get searchForm(): FormGroup {
    return this.weatherFactoryService.searchForm;
  }

  get searchValueControl(): FormControl {
    return this.searchForm?.get('searchValue') as FormControl;
  }
}
