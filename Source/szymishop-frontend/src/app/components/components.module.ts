import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeModule } from './home/home.module';
import { AuthModule } from './auth/auth.module';
import { ProductListModule } from './product-list/product-list.module';
import { NavbarModule } from './navbar/navbar.module';



@NgModule({
  imports: [
    CommonModule,
    HomeModule,
    AuthModule,
    ProductListModule,
    NavbarModule
  ],
  exports: [
    NavbarModule
  ]
})
export class ComponentsModule { }
