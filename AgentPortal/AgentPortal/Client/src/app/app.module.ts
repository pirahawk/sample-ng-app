import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppcommonModule } from './appcommon/appcommon.module';
import { NavigationComponent } from './components/navigation/navigation.component';
import { ListingsComponent } from './components/listings/listings.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    ListingsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AppcommonModule,
  ],
  providers: [],
  bootstrap: [AppComponent, NavigationComponent]
})
export class AppModule { }
