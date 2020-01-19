import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListingImagePreviewComponent } from './listing-image-preview.component';

describe('ListingImagePreviewComponent', () => {
  let component: ListingImagePreviewComponent;
  let fixture: ComponentFixture<ListingImagePreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListingImagePreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListingImagePreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
