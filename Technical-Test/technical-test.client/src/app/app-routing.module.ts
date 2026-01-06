import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ListProductComponent } from './Pages/list-product/list-product.component';
import { AddProductComponent } from './Pages/add-product/add-product.component';

const routes: Routes = [
  { path: '', component: ListProductComponent },
  { path: 'list-product', component: ListProductComponent },
  { path: 'add-product', component: AddProductComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule] 
})
export class AppRoutingModule { }
