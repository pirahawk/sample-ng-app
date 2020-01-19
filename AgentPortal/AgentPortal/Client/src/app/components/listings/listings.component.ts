import { Component, OnInit } from '@angular/core';
import { GetListingsService } from 'src/app/services/get-listings.service';
import { ListingResponse } from 'src/app/domain/http/listingResponse';

@Component({
  selector: 'app-listings',
  templateUrl: './listings.component.html'
})
export class ListingsComponent implements OnInit {

  public listings:ListingResponse[];
  public isRetrievingListings: boolean;

  constructor(private getListings:GetListingsService) { }

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
    if(!response.links || !response.links.length){
      return null;
    }

    for(let link of response.links){
        if(link.relation === "IMAGES"){
            return link.href;
        }
    }
  }

}
