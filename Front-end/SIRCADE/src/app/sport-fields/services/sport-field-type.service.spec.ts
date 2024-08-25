import { TestBed } from '@angular/core/testing';

import { SportFieldTypeService } from './sport-field-type.service';

describe('SportFieldTypeService', () => {
  let service: SportFieldTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SportFieldTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
