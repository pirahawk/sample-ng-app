import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListingsComponent } from './components/listings/listings.component';
import { NotFoundComponent } from './appcommon/components/not-found/not-found.component';


const routes: Routes = [
  {
    path: 'listings',
    component: ListingsComponent
  },
  {
    path: '',
    component: ListingsComponent
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
