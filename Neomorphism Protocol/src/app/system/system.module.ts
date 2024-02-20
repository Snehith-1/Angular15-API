import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SystemRoutingModule } from './system-routing.module';
import { EntityComponent } from './master/entity/entity.component';
import { EmployeeComponent } from './master/employee/employee.component';
import { MaterialModule } from 'src/material/material.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { AddentityComponent } from './master/popup/addentity/addentity.component';
import { AddemployeeComponent } from './master/popup/addemployee/addemployee.component';
import { DeleteentityComponent } from './master/popup/deleteentity/deleteentity.component';


@NgModule({
  declarations: [
    EntityComponent,
    EmployeeComponent,
    AddentityComponent,
    AddemployeeComponent,
    DeleteentityComponent
  ],
  imports: [
    CommonModule,
    SystemRoutingModule,MaterialModule,MatPaginatorModule ,MatTableModule ,
  ]
})
export class SystemModule { }
