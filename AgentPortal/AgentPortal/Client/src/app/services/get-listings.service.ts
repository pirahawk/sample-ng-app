import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import {Observable, observable, of} from 'rxjs';
import { ListingResponse } from '../domain/http/listingResponse';

@Injectable({
  providedIn: 'root'
})
export class GetListingsService {
  constructor(private httpClient: HttpClient) { }

  public getAllListing():Observable<ListingResponse[]>{
    return this.httpClient.get<ListingResponse[]>("api/listings");
  }

  public getListing(listingHref:string):Observable<ListingResponse>{
    return this.httpClient.get<ListingResponse>(listingHref);
  }

  public addListing(newListing:any):Observable<HttpResponse<any>>{
    return this.httpClient.post("api/listings", newListing, {observe: 'response'});
  }

  public updateListing(listingHref:string, update:any):Observable<HttpResponse<any>>{
    return this.httpClient.put(listingHref, update, {observe: 'response'});
  }
}
