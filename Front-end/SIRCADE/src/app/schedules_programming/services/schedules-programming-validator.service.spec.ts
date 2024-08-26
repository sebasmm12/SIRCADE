import { TestBed } from '@angular/core/testing';

import { SchedulesProgrammingValidatorService } from './schedules-programming-validator.service';

describe('SchedulesProgrammingValidatorService', () => {
  let service: SchedulesProgrammingValidatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SchedulesProgrammingValidatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
