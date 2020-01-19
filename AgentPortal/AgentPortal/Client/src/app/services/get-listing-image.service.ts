import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListingImageResponse } from '../domain/http/listingImageResponse';

@Injectable({
  providedIn: 'root'
})
export class GetListingImageService {

  constructor(private httpClient: HttpClient) { }

  public getAllListingImages(imageHref:string):Observable<ListingImageResponse[]>{
    return this.httpClient.get<ListingImageResponse[]>(imageHref);
  }
}
