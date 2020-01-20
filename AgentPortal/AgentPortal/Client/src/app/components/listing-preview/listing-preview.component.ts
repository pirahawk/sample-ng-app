import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Navigation } from '@angular/router';
import { GetListingsService } from 'src/app/services/get-listings.service';
import { GetListingImageService } from 'src/app/services/get-listing-image.service';
import { ListingResponse } from 'src/app/domain/http/listingResponse';
import { ListingImageResponse } from 'src/app/domain/http/listingImageResponse';

@Component({
  selector: 'app-listing-preview',
  templateUrl: './listing-preview.component.html'
})
export class ListingPreviewComponent implements OnInit {

  private listingHref:string;
  private imageHref:string;
  public listing:ListingResponse;
  public images:ListingImageResponse[];

  constructor(private route: ActivatedRoute,
    private router: Router,
    private getListings:GetListingsService,
    private getListingImageService: GetListingImageService
    ) {
    
    const navigation:Navigation = this.router.getCurrentNavigation();
    const state:any = navigation.extras.state;

    this.listingHref = state.listingHref;
    this.imageHref = state.imageHref;
  }

  ngOnInit() {
    this.getListings.getListing(this.listingHref)
    .subscribe(
      (response:ListingResponse) => this.listing = response,
      (error:any)=>{}
    );

    this.getListingImageService.getAllListingImages(this.imageHref)
    .subscribe(
      (response:ListingImageResponse[]) => this.images = response,
      (error:any)=>{}
    );
  }

}
