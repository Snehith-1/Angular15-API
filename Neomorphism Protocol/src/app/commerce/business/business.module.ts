import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BusinessRoutingModule } from './business-routing.module';
import { BusinessComponent } from './business.component';


@NgModule({
  declarations: [
    BusinessComponent
  ],
  imports: [
    CommonModule,
    BusinessRoutingModule
  ]
})
export class BusinessModule { }
