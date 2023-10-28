import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewWeatherInformationComponent } from './view-weather-information.component';

describe('ViewWeatherInformationComponent', () => {
  let component: ViewWeatherInformationComponent;
  let fixture: ComponentFixture<ViewWeatherInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewWeatherInformationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewWeatherInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
