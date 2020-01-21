import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Navigation } from '@angular/router';
import { GetListingsService } from 'src/app/services/get-listings.service';
import { GetListingImageService } from 'src/app/services/get-listing-image.service';
import { ListingResponse } from 'src/app/domain/http/listingResponse';
import { ListingImageResponse } from 'src/app/domain/http/listingImageResponse';
import { HttpResponse } from '@angular/common/http';
import { ResponseHelperService } from 'src/app/services/response-helper.service';

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
    private getListingImageService: GetListingImageService,
    private responseHelper:ResponseHelperService) {
    
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

  public expireListing(event:any):void{
    var updateRequest:any = {};
    Object.assign(updateRequest, this.listing);

    updateRequest.expired = true;

    this.getListings.updateListing(this.listingHref, updateRequest)
    .subscribe(
      (response:HttpResponse<any>) => this.router.navigate(["listings"]),
      (error:any)=>{}
    );
  }

  public editListing(event:any):void{
    let stateArg:any = {
      listing: this.listing
    };

    this.router.navigate(["edit", this.listing.id], {state:stateArg});
  }
}
