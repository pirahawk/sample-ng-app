import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ListingsComponent } from "./components/listings/listings.component";
import { NotFoundComponent } from "./appcommon/components/not-found/not-found.component";
import { ListingPreviewComponent } from "./components/listing-preview/listing-preview.component";
import { ListingEditorComponent } from "./components/listing-editor/listing-editor.component";


const routes: Routes = [
  {
    path: "listing/:id",
    component: ListingPreviewComponent,
  },
  {
    path: "edit/:id",
    component: ListingEditorComponent,
  },
  {
    path: "add-listing",
    component: ListingEditorComponent,
  },
  {
    path: "listings",
    component: ListingsComponent
  },
  {
    path: "",
    component: ListingsComponent
  },
  {
    path: "**",
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
