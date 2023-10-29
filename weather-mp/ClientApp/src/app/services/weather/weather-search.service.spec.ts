import { TestBed } from '@angular/core/testing';

import { WeatherSearchService } from './weather-search.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';

describe('WeatherSearchService', () => {
  let service: WeatherSearchService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, ReactiveFormsModule],
      providers: [{ provide: 'BASE_URL', useValue: '/' }],
    });
    service = TestBed.inject(WeatherSearchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
