import { TestBed } from '@angular/core/testing';

import { AccountsValidatorService } from './accounts-validator.service';

describe('AccountsValidatorService', () => {
  let service: AccountsValidatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccountsValidatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
