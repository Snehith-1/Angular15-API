import { TestBed } from '@angular/core/testing';

import { ApplicationSummaryService } from './application-summary.service';

describe('ApplicationSummaryService', () => {
  let service: ApplicationSummaryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApplicationSummaryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
