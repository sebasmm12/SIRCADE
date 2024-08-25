import { TestBed } from '@angular/core/testing';

import { SportFieldsValidatorService } from './sport-fields-validator.service';

describe('SportFieldsValidatorService', () => {
  let service: SportFieldsValidatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SportFieldsValidatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
