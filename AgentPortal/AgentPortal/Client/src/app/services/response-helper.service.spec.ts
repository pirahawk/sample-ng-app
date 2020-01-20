import { TestBed } from '@angular/core/testing';

import { ResponseHelperService } from './response-helper.service';

describe('ResponseHelperService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ResponseHelperService = TestBed.get(ResponseHelperService);
    expect(service).toBeTruthy();
  });
});
