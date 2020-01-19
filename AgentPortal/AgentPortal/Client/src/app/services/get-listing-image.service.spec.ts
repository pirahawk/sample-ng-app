import { TestBed } from '@angular/core/testing';

import { GetListingImageService } from './get-listing-image.service';

describe('GetListingImageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GetListingImageService = TestBed.get(GetListingImageService);
    expect(service).toBeTruthy();
  });
});
