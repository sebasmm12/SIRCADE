import { TestBed } from '@angular/core/testing';

import { SchedulesProgrammingService } from './schedules-programming.service';

describe('SchedulesProgrammingService', () => {
  let service: SchedulesProgrammingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SchedulesProgrammingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
