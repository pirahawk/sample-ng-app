import { Injectable } from '@angular/core';
import { ListingResponse } from '../domain/http/listingResponse';

@Injectable({
  providedIn: 'root'
})
export class ResponseHelperService {

  constructor() { }

  public getSelfHref(response: ListingResponse):string{
    return this.tryGetLink(response, "SELF");
  }

  public getImageHref(response: ListingResponse):string{
    return this.tryGetLink(response, "IMAGES");
  }

  private tryGetLink(response: ListingResponse, relation:string):string{
    if(!response.links || !response.links.length){
      return null;
    }

    for(let link of response.links){
        if(link.relation === relation){
            return link.href;
        }
    }
  }
}
