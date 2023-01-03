import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



const modules = [
  FormsModule,
  ReactiveFormsModule,
  MatButtonModule,
  MatMenuModule,
  MatInputModule,
  MatDialogModule
]

@NgModule({
  declarations: [],
  imports: modules,
  exports: modules
})
export class SharedModule { }
