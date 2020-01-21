import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Navigation } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';
import { ListingFormValidators } from 'src/app/domain/validation/listingFormValidators';
import { GetListingsService } from 'src/app/services/get-listings.service';
import { ListingResponse } from 'src/app/domain/http/listingResponse';
import { HttpResponse } from '@angular/common/http';
import { ResponseHelperService } from 'src/app/services/response-helper.service';
import { GetListingImageService } from 'src/app/services/get-listing-image.service';

@Component({
  selector: 'app-listing-editor',
  templateUrl: './listing-editor.component.html'
})
export class ListingEditorComponent implements OnInit {
  public listing: ListingResponse;

  public numberRoomsControl: FormControl;
  public postcodeControl: FormControl;
  public addressControl: FormControl;
  public descriptionControl: FormControl;
  public askingPriceControl: FormControl;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private getListings: GetListingsService,
    private getListingImageService: GetListingImageService,
    private responseHelper: ResponseHelperService) {

    const navigation: Navigation = this.router.getCurrentNavigation();
    
    if(!navigation || !navigation.extras || !navigation.extras.state){
      return;
    }

    const state: any = navigation.extras.state;
    this.listing = state.listing;
  }

  ngOnInit() {
    this.descriptionControl = new FormControl(this.listing ? this.listing.description : '', [Validators.required]);
    this.addressControl = new FormControl(this.listing ? this.listing.address : '', [Validators.required]);
    this.postcodeControl = new FormControl(this.listing ? this.listing.postCode : '', [ListingFormValidators.matchesValidUKPostCode()]);

    this.numberRoomsControl = new FormControl(this.listing ? this.listing.numberBedrooms : 0, Validators.compose([
      Validators.required,
      Validators.min(1),
      Validators.pattern(/^\d+$/)]));

    this.askingPriceControl = new FormControl(this.listing ? this.listing.askingPrice : 0,
      Validators.compose([Validators.required,
      Validators.min(1),
      Validators.pattern(/^\d+$/)]));
  }

  public canSubmit(): boolean {
    let isValid: boolean = this.descriptionControl.valid
      && this.addressControl.valid
      && this.postcodeControl.valid
      && this.numberRoomsControl.valid
      && this.askingPriceControl.valid;

    return isValid;
  }

  public submitListing(event: any): void {

    let newListing: ListingResponse = new ListingResponse();
    newListing.description = this.descriptionControl.value;
    newListing.address = this.addressControl.value;
    newListing.postCode = this.postcodeControl.value;
    newListing.numberBedrooms = this.numberRoomsControl.value;
    newListing.askingPrice = this.askingPriceControl.value;

    if (this.listing) {
      this.updateExistingListing(newListing);
      return;
    }

    this.addNewListing(newListing);
  }

  private addNewListing(newListing: ListingResponse) {
    this.getListings.addListing(newListing)
      .subscribe((response: HttpResponse<any>) => this.router.navigate(["listings"]),
        (error: any) => { });
  }

  private updateExistingListing(updatedListing: ListingResponse) {
    let listingHref: string = this.responseHelper.getSelfHref(this.listing);

    this.getListings.updateListing(listingHref, updatedListing)
      .subscribe((response: HttpResponse<any>) => this.router.navigate(["listings"]),
        (error: any) => { });
  }

  public navigateBack(event:any):void{
    if(!this.listing){
      this.router.navigate(["listings"]);
      return;
    }

    let stateArg:any = {
      listingHref: this.responseHelper.getSelfHref(this.listing),
      imageHref: this.responseHelper.getImageHref(this.listing)
    };

    this.router.navigate(["listing", this.listing.id], {state:stateArg});
  }

  public uploadImage(event:any):void{
    let input: HTMLInputElement = document.createElement('input');
    input.type = 'file';
    input.accept = 'image/jpeg';
    
    input.addEventListener('change', (event:Event)=>{
      
      let target:HTMLInputElement = event.target as HTMLInputElement;
      
      if(target.files){
        let uploadImg:File = target.files[0];
        let formData = new FormData();
        formData.append('file', uploadImg);
        let imageHref:string =  this.responseHelper.getImageHref(this.listing);

        this.getListingImageService.addListingImage(imageHref, formData)
        .subscribe(
          (state:any)=>{
            let test = 1;
          },
          (error:any)=>{
            let test = 1;
          },
        );
      }
    });
    input.click();
    
  }
}
