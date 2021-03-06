import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppcommonModule } from './appcommon/appcommon.module';
import { NavigationComponent } from './components/navigation/navigation.component';
import { ListingsComponent } from './components/listings/listings.component';
import { GetListingsService } from './services/get-listings.service';
import { ListingImagePreviewComponent } from './components/listing-image-preview/listing-image-preview.component';
import { GetListingImageService } from './services/get-listing-image.service';
import { ResponseHelperService } from './services/response-helper.service';
import { ListingPreviewComponent } from './components/listing-preview/listing-preview.component';
import { ListingEditorComponent } from './components/listing-editor/listing-editor.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    ListingsComponent,
    ListingImagePreviewComponent,
    ListingPreviewComponent,
    ListingEditorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AppcommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'en-GB'},
    GetListingsService,
    GetListingImageService,
    ResponseHelperService
  ],
  bootstrap: [AppComponent, NavigationComponent]
})
export class AppModule { }
