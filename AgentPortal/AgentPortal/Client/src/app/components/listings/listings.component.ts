import { Component, OnInit } from '@angular/core';
import { GetListingsService } from 'src/app/services/get-listings.service';
import { ListingResponse } from 'src/app/domain/http/listingResponse';

@Component({
  selector: 'app-listings',
  templateUrl: './listings.component.html',
  styleUrls: ['./listings.component.scss']
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

}
