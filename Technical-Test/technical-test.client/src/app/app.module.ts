import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';
import { ProductListComponent } from './Components/product-list/product-list.component';
import { AppRoutingModule } from './app-routing.module';
import { ListProductComponent } from './Pages/list-product/list-product.component';
import { AddProductComponent } from './Pages/add-product/add-product.component';
import { DropdownComponent } from './Components/dropdown/dropdown.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    ProductListComponent,
    ListProductComponent,
    AddProductComponent,
    DropdownComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, AppRoutingModule, ReactiveFormsModule, FormsModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
