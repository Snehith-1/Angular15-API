import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { AdminComponent } from './admin.component';
import { InternalServerErrorComponent } from './componet/internal-server-error/internal-server-error.component';
import { LoginComponent } from './componet/login/login.component';
import { PageNotFoundComponent } from './componet/page-not-found/page-not-found.component';
import { SsoresponseComponent } from './componet/ssoresponse/ssoresponse.component';
import { AdvanceHomePageContentComponent } from './componet/welcome/advance-home-page-content/advance-home-page-content.component';
import { ApplicationViewComponent } from './componet/welcome/application-view/application-view.component';
import { WelcomeComponent } from './componet/welcome/welcome.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'welcome', component: WelcomeComponent },
  { path: "response", component: SsoresponseComponent },
  { path: 'dashboard', component: AdvanceHomePageContentComponent},
  { path: 'applicationview', component: ApplicationViewComponent},
  { path: '500', component:InternalServerErrorComponent},
  { path: 'error', component:PageNotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
