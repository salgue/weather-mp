import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewWeatherInformationComponent } from './view-weather-information.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from 'src/app/material/material.module';
import { DialogService } from 'src/app/services/dialog/dialog.service';
import { MatDialogModule, MAT_DIALOG_DATA } from '@angular/material/dialog';

describe('ViewWeatherInformationComponent', () => {
  let component: ViewWeatherInformationComponent;
  let fixture: ComponentFixture<ViewWeatherInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewWeatherInformationComponent],
      imports: [
        BrowserAnimationsModule,
        ReactiveFormsModule,
        MaterialModule,
        HttpClientTestingModule,
        MatDialogModule,
      ],
      providers: [
        { provide: 'BASE_URL', useValue: '/' },
        DialogService,
        { provide: MAT_DIALOG_DATA, useValue: {} },
      ],
      errorOnUnknownElements: true,
      errorOnUnknownProperties: true,
    }).compileComponents();

    fixture = TestBed.createComponent(ViewWeatherInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
