import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import {MatButtonModule} from '@angular/material/button';
import {MatDividerModule} from '@angular/material/divider';
import {MatIconModule} from '@angular/material/icon';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatCardModule} from '@angular/material/card';
import {MatSelectModule} from '@angular/material/select';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatTableModule} from '@angular/material/table'; 
import { MatPaginatorModule } from '@angular/material/paginator';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatTabsModule} from '@angular/material/tabs';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import { MatSortModule } from '@angular/material/sort';
import {MatRadioModule} from '@angular/material/radio';
import { MatMenuModule} from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepicker, MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import {MatExpansionModule} from '@angular/material/expansion';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AngularEditorModule } from '@kolkov/angular-editor';




@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule, 
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
    MatToolbarModule,
    MatTableModule,
    MatPaginatorModule,
    MatSlideToggleModule,
    MatTabsModule,
    MatProgressSpinnerModule,
    MatSidenavModule,
    MatListModule,
    MatSortModule,
    MatRadioModule,
    MatMenuModule,
    MatDialogModule, 
    MatDatepickerModule,
    MatNativeDateModule,MatExpansionModule,AngularEditorModule,
    ],
  exports:[
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
    MatToolbarModule,
    MatTableModule,
    MatPaginatorModule,
    MatSlideToggleModule,
    MatTabsModule,
    MatProgressSpinnerModule,
    MatSidenavModule,
    MatListModule,
    MatSortModule,
    MatRadioModule,
    MatMenuModule,
    MatDialogModule,
    MatDatepickerModule,MatExpansionModule,MatCheckboxModule,AngularEditorModule,
  ]
})
export class MaterialModule {
  
 }
