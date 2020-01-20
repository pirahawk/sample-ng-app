import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';
import { ListingFormValidators } from 'src/app/domain/validation/listingFormValidators';

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
  
  constructor(private route: ActivatedRoute, private router: Router) {}

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

  public testValid(): string {
    let str: string = `${this.numberRoomsControl.value} ${this.askingPriceControl.value}  ${this.canSubmit()}`;
    return str;
  }

  public canSubmit():boolean{
    let isValid: boolean = this.descriptionControl.valid
    && this.addressControl.valid
    && this.postcodeControl.valid
    && this.numberRoomsControl.valid
    && this.askingPriceControl.valid;

    return isValid;
  }
}
