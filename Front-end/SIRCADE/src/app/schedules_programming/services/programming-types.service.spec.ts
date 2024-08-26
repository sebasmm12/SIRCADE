import { TestBed } from '@angular/core/testing';

import { ProgrammingTypesService } from './programming-types.service';

describe('ProgrammingTypesService', () => {
  let service: ProgrammingTypesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProgrammingTypesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
