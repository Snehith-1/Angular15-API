import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { WelcomeComponent } from './component/welcome/welcome.component';
import { DashboardComponent } from './dashboard.component';
import { MenuComponent } from '../admin/componet/menu/menu.component';
import { AdminModule } from '../admin/admin.module';


@NgModule({
  declarations: [
    WelcomeComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    AdminModule
  ]
})
export class DashboardModule { }
