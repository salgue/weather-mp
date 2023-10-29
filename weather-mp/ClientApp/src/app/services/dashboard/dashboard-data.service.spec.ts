import { TestBed } from '@angular/core/testing';

import { DashboardDataService } from './dashboard-data.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';

describe('DashboardDataService', () => {
  let service: DashboardDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, ReactiveFormsModule],
      providers: [{ provide: 'BASE_URL', useValue: '/' }],
    });
    service = TestBed.inject(DashboardDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
