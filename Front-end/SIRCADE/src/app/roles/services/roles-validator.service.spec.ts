import { TestBed } from '@angular/core/testing';

import { RolesValidatorService } from './roles-validator.service';

describe('RolesValidatorService', () => {
  let service: RolesValidatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RolesValidatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
