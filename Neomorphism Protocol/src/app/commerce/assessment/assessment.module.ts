import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssessmentRoutingModule } from './assessment-routing.module';
import { AssessmentComponent } from './assessment.component';


@NgModule({
  declarations: [
    AssessmentComponent
  ],
  imports: [
    CommonModule,
    AssessmentRoutingModule
  ]
})
export class AssessmentModule { }
