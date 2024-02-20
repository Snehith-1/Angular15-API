import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Interceptor } from './admin/services/interceptor.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AdminModule } from './admin/admin.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { DatePipe } from '@angular/common';
import { UikitComponent } from './shared/component/uikit/uikit.component';

import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MaterialModule } from 'src/material/material.module';
@NgModule({
  declarations: [
    AppComponent,
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AdminModule,
    DashboardModule,MatPaginatorModule,MatSortModule,MatTableModule,MatInputModule,MaterialModule,
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS, useClass: Interceptor, multi: true
  }, DatePipe, Ng2SearchPipeModule, UikitComponent
 ],
  bootstrap: [AppComponent]
})
export class AppModule { }
