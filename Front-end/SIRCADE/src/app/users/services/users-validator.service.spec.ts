import { TestBed } from '@angular/core/testing';

import { UsersValidatorService } from './users-validator.service';

describe('UsersValidatorService', () => {
  let service: UsersValidatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UsersValidatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
