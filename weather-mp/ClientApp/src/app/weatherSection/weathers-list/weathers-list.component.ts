import { Component, OnDestroy } from '@angular/core';
import { WeatherDataService } from 'src/app/services/weather/weather-data.service';

@Component({
  selector: 'app-weathers-list',
  templateUrl: './weathers-list.component.html',
  styleUrls: ['./weathers-list.component.css'],
})
export class WeathersListComponent implements OnDestroy {
  constructor(
    private weatherDataService: WeatherDataService
  ) { }
  
  ngOnDestroy(): void {
    this.weatherDataService.resetValues();
  }

  get dailyForecast(): any[] {
    return this.weatherDataService.dailyForecast;
  }
}
