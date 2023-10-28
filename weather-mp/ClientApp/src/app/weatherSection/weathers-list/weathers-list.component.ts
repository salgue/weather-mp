import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { DialogService } from 'src/app/services/dialog/dialog.service';
import { WeatherDataService } from 'src/app/services/weather/weather-data.service';
import { WeatherFormFactoryService } from 'src/app/services/weather/weather-form-factory.service';

@Component({
  selector: 'app-weathers-list',
  templateUrl: './weathers-list.component.html',
  styleUrls: ['./weathers-list.component.css'],
})
export class WeathersListComponent {
  constructor(
    private weatherDataService: WeatherDataService
  ) {}

  openWeatherModal() {

  }

  get dailyForecast(): any[] {
    return this.weatherDataService.dailyForecast;
  }

}
