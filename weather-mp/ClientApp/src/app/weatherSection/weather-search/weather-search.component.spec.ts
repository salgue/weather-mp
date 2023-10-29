import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule} from '@angular/common/http/testing';
import { WeatherSearchComponent } from './weather-search.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/material/material.module';
import { By } from '@angular/platform-browser';
import { WeatherDataService } from 'src/app/services/weather/weather-data.service';

describe('WeatherSearchComponent', () => {
  let component: WeatherSearchComponent;
  let fixture: ComponentFixture<WeatherSearchComponent>;
  let service: WeatherDataService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WeatherSearchComponent],
      imports: [
        BrowserAnimationsModule,
        ReactiveFormsModule,
        MaterialModule,
        HttpClientTestingModule
      ],
      providers: [
      {provide: 'BASE_URL', useValue:'/'}
      ],
      errorOnUnknownElements: true,
      errorOnUnknownProperties: true
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeatherSearchComponent);
    component = fixture.componentInstance;
    service = TestBed.inject(WeatherDataService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display search input', () => {
    const element = fixture.debugElement.query(By.css('.search-input')).nativeElement;

    expect(element).not.toBeNull();
  });

   it('should display search input', () => {
     const element = fixture.debugElement.query(
       By.css('.search-input')
     ).nativeElement;

     expect(element).not.toBeNull();
   });
  
   it('should display search button', () => {
     const element = fixture.debugElement.query(
       By.css('.search-button')
     ).nativeElement;

     expect(element).not.toBeNull();
   });
  
   it('should display favorite button', () => {
     const element = fixture.debugElement.query(
       By.css('.favorite-button')
     ).nativeElement;

     expect(element).not.toBeNull();
   });
  
  it('should call search method', () => {
    spyOn(component, 'search');
    const element = fixture.debugElement.query(
      By.css('.search-button')
    ).nativeElement;
    
    element.click();
    expect(component.search).toHaveBeenCalled();
  })

    it('should call changeFavoriteValue method', () => {
      spyOn(component, 'changeFavoriteValue');
      const element = fixture.debugElement.query(
        By.css('.favorite-button')
      ).nativeElement;

      element.click();
      expect(component.changeFavoriteValue).toHaveBeenCalled();
    });
  
});
