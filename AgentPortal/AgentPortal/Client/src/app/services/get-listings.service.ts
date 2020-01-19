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
}


// this.httpClient.get("/health", {responseType: 'text', observe: 'response'})
    // .subscribe((response: any)=>{
    //   let responseTest = response;
    // },
    // (error:HttpErrorResponse)=>{
    //   let errorTest = error;
    // });
