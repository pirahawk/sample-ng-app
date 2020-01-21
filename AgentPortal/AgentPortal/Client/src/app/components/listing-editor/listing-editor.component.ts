import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';
import { ListingFormValidators } from 'src/app/domain/validation/listingFormValidators';
import { GetListingsService } from 'src/app/services/get-listings.service';
import { ListingResponse } from 'src/app/domain/http/listingResponse';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-listing-editor',
  templateUrl: './listing-editor.component.html'
})
export class ListingEditorComponent implements OnInit {

  public numberRoomsControl: FormControl;
  public postcodeControl: FormControl;
  public addressControl: FormControl;
  public descriptionControl: FormControl;
  public askingPriceControl: FormControl;
  
  constructor(private route: ActivatedRoute, 
    private router: Router,
    private getListings:GetListingsService) {}

  ngOnInit() {
    this.descriptionControl = new FormControl('', [Validators.required]);
    this.addressControl = new FormControl('', [Validators.required]);
    this.postcodeControl = new FormControl('', [ListingFormValidators.matchesValidUKPostCode()]);
    
    this.numberRoomsControl = new FormControl( 0, Validators.compose([
      Validators.required,
      Validators.min(1),
      Validators.pattern(/^\d+$/)]));

    this.askingPriceControl = new FormControl(0,
      Validators.compose([Validators.required,
      Validators.min(1),
      Validators.pattern(/^\d+$/)]));
  }

  public canSubmit():boolean{
    let isValid: boolean = this.descriptionControl.valid
    && this.addressControl.valid
    && this.postcodeControl.valid
    && this.numberRoomsControl.valid
    && this.askingPriceControl.valid;

    return isValid;
  }

  public submitListing(event:any):void {

    let newListing = new ListingResponse();
    newListing.description = this.descriptionControl.value;
    newListing.address = this.addressControl.value;
    newListing.postCode = this.postcodeControl.value;
    newListing.numberBedrooms = this.numberRoomsControl.value;
    newListing.askingPrice = this.askingPriceControl.value;

    this.addNewListing(newListing);
  }

  private addNewListing(newListing: ListingResponse) {
    this.getListings.addListing(newListing)
      .subscribe((response: HttpResponse<any>) => this.router.navigate(["listings"]), 
      (error: any) => { });
  }
}
