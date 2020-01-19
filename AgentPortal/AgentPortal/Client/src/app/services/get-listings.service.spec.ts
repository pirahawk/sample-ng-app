import { TestBed } from '@angular/core/testing';

import { GetListingsService } from './get-listings.service';

describe('GetListingsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GetListingsService = TestBed.get(GetListingsService);
    expect(service).toBeTruthy();
  });
});
