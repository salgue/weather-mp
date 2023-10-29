import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeatherCardComponent } from './weather-card.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from 'src/app/material/material.module';

describe('WeatherCardComponent', () => {
  let component: WeatherCardComponent;
  let fixture: ComponentFixture<WeatherCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WeatherCardComponent],
      imports: [
        BrowserAnimationsModule,
        ReactiveFormsModule,
        MaterialModule,
        HttpClientTestingModule,
      ],
      providers: [{ provide: 'BASE_URL', useValue: '/' }],
      errorOnUnknownElements: true,
      errorOnUnknownProperties: true,
    }).compileComponents();

    fixture = TestBed.createComponent(WeatherCardComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
        component.weather = {
          date: '10/28/2023',
          minTemperature: 10,
          maxTemperature: 15,
          icon: '',
          description: '',
          speed: '',
          temperature: 10,
    };
    
    fixture.detectChanges();
    
    expect(component).toBeTruthy();
  });
});
