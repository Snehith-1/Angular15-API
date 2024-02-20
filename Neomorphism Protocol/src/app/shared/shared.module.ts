import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UikitComponent } from './component/uikit/uikit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    UikitComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  exports:[
    UikitComponent
  ]
})
export class SharedModule { }
