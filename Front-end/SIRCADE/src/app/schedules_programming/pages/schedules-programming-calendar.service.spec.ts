import { TestBed } from '@angular/core/testing';

import { SchedulesProgrammingCalendarService } from './schedules-programming-calendar.service';

describe('SchedulesProgrammingCalendarService', () => {
  let service: SchedulesProgrammingCalendarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SchedulesProgrammingCalendarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
