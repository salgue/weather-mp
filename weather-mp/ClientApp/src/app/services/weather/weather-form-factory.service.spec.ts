import { TestBed } from '@angular/core/testing';

import { WeatherFormFactoryService } from './weather-form-factory.service';

describe('WeatherFormFactoryService', () => {
  let service: WeatherFormFactoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WeatherFormFactoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
