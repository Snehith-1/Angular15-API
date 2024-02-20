import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CommerceRoutingModule } from './commerce-routing.module';
import { ProductSamagroComponent } from './product-samagro/product-samagro.component';


@NgModule({
  declarations: [
    ProductSamagroComponent
  ],
  imports: [
    CommonModule,
    CommerceRoutingModule
  ]
})
export class CommerceModule { }
