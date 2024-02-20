import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EntityComponent } from './master/entity/entity.component';
import { EmployeeComponent } from './master/employee/employee.component';
import { AddemployeeComponent } from './master/popup/addemployee/addemployee.component';

const routes: Routes = [
   {
    path: 'master',
    children: [
    {
      path: 'entity',
      component: EntityComponent
    },
    { path: 'employee', 
    component: EmployeeComponent
    },
    {
      path: 'popup',
     children:
     [
      {path: 'addemployee',component: AddemployeeComponent},
      // {path: 'edittermsandconditions/:termsconditions_gid',component: EdittermsandconditionsComponent},
  
  
    ]
    },
 
    
    ]
   
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SystemRoutingModule { }
