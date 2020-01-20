import { ValidatorFn, AbstractControl } from '@angular/forms';

export class ListingFormValidators{
    private static postCodeRegex: RegExp = /^(?<outbound>[a-zA-Z](\d{1,2}|[a-zA-Z]\d{1,2}|\d[a-zA-Z]|[a-zA-Z]\d[a-zA-Z]))\s?(?<inbound>\d[a-zA-Z]{2})$/;

    public static matchesValidUKPostCode(): ValidatorFn{
        return (control: AbstractControl): { [key:string]: any } | null =>{
            if(!control.value) {
                return {'required': {value: control.value}};
            }

            let isValidPostcode: boolean = this.postCodeRegex.test(control.value);
            return !isValidPostcode? {'invalid': {value: control.value}} : null;
        };
    }
}