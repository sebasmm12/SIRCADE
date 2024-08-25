import { TestBed } from '@angular/core/testing';

import { SportFieldsService } from './sport-fields.service';

describe('SportFieldsService', () => {
  let service: SportFieldsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SportFieldsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
