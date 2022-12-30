import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NavbarComponent } from './navbar.component';
import { SharedModule } from '../../shared/shared.module';
import { AuthModule } from '../auth/auth.module';



@NgModule({
  declarations: [
    NavbarComponent
  ],
  exports: [
    NavbarComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    AuthModule
  ]
})
export class NavbarModule { }
