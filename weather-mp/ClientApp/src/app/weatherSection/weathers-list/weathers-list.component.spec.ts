import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeathersListComponent } from './weathers-list.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from 'src/app/material/material.module';
import { NotFoundComponent } from 'src/app/shared/not-found/not-found.component';

describe('WeathersListComponent', () => {
  let component: WeathersListComponent;
  let fixture: ComponentFixture<WeathersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WeathersListComponent, NotFoundComponent],
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

    fixture = TestBed.createComponent(WeathersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
