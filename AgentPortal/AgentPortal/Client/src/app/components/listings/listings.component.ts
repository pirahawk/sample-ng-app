import { Component, OnInit } from '@angular/core';
import { GetListingsService } from 'src/app/services/get-listings.service';
import { ListingResponse } from 'src/app/domain/http/listingResponse';
import { ResponseHelperService } from 'src/app/services/response-helper.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-listings',
  templateUrl: './listings.component.html'
})
export class ListingsComponent implements OnInit {

  public listings:ListingResponse[];
  public isRetrievingListings: boolean;

  constructor(private getListings:GetListingsService, 
    private responseHelper:ResponseHelperService,
    private router: Router) { }

  ngOnInit() {
    this.isRetrievingListings = true;

    this.getListings.getAllListing()
    .subscribe(
      (response:ListingResponse[])=> this.listings = response,
      (error: any)=> this.listings = null,
      ()=> this.isRetrievingListings = false
    );
  }

  public getImageHref(response: ListingResponse):string{
    return this.responseHelper.getImageHref(response);
  }

  public navigateListing(event:any, listing:ListingResponse):void{
    let stateArg:any = {
      listingHref: this.responseHelper.getSelfHref(listing),
      imageHref: this.responseHelper.getImageHref(listing)
    };

    this.router.navigate(["listing", listing.id], {state:stateArg});
  }
}
