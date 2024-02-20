import { TestBed } from '@angular/core/testing';

import { SsoresponseService } from './ssoresponse.service';

describe('SsoresponseService', () => {
  let service: SsoresponseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SsoresponseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
