import { TestBed } from '@angular/core/testing';

import { ApplicationCreationService } from './application-creation.service';

describe('ApplicationCreationService', () => {
  let service: ApplicationCreationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApplicationCreationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
