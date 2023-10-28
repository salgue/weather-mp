import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeathersListComponent } from './weathers-list.component';

describe('WeathersListComponent', () => {
  let component: WeathersListComponent;
  let fixture: ComponentFixture<WeathersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WeathersListComponent ]
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
