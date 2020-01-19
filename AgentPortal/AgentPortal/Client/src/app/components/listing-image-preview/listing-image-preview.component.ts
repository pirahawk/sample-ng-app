import { Component, OnInit, Input } from '@angular/core';
import { GetListingImageService } from 'src/app/services/get-listing-image.service';
import { ListingImageResponse } from 'src/app/domain/http/listingImageResponse';

@Component({
  selector: 'app-listing-image-preview',
  templateUrl: './listing-image-preview.component.html'
})
export class ListingImagePreviewComponent implements OnInit {

  @Input() imageHref: string;
  public listingPreviewImageUrl: string;

  constructor(private getListingImageService: GetListingImageService) { }

  ngOnInit() {
    if(!this.imageHref){
      return;
    }

    this.getListingImageService.getAllListingImages(this.imageHref)
    .subscribe(
      (response:ListingImageResponse[]) => this.setPreviewImage(response),
      (err:any)=>{});
  }

  setPreviewImage(response: ListingImageResponse[]): void {
    if(!response || response.length === 0){
      this.listingPreviewImageUrl = '';
      return;
    }

    this.listingPreviewImageUrl = response[0].url;
  }
}
