import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin/admin.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FinanceComponent } from './finance/finance.component';
import { UikitComponent } from './shared/component/uikit/uikit.component';

const routes: Routes = [
  { path: '', redirectTo: 'page/login', pathMatch: 'full' },
  // {  path: "**", redirectTo: 'page/500' },
  {
    path:'page',
    component:AdminComponent,
    loadChildren:() =>import('./admin/admin.module').then(x=>x.AdminModule)
  },
  {
    path:'app',
    component:DashboardComponent,
    loadChildren:() =>import('./dashboard/dashboard.module').then(x=>x.DashboardModule)
  },
  {
    path:'app',
    component:FinanceComponent,
    loadChildren:() =>import('./finance/finance.module').then(x=>x.FinanceModule)
  },
  {
    path:'system',
    loadChildren:() =>import('./system/system.module').then(x=>x.SystemModule)
  },
  {
    path:'app/uikit',
    component: UikitComponent,
  },
  {
    path: "**",    
    redirectTo: "page/login",    
  },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash:true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
