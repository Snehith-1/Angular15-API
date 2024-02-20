import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { LoginComponent } from './componet/login/login.component';
import { WelcomeComponent } from './componet/welcome/welcome.component';
import { FormsModule, FormArray, FormControl, FormBuilder, Validators } from '@angular/forms';
import { FooterComponent } from './componet/footer/footer.component';
import { MenuComponent } from './componet/menu/menu.component';
import { AdvanceHomePageContentComponent } from './componet/welcome/advance-home-page-content/advance-home-page-content.component';
import { ApplicationViewComponent } from './componet/welcome/application-view/application-view.component';
import { SsoresponseComponent } from './componet/ssoresponse/ssoresponse.component';
import { InternalServerErrorComponent } from './componet/internal-server-error/internal-server-error.component';
import { PageNotFoundComponent } from './componet/page-not-found/page-not-found.component';


@NgModule({
  declarations: [
    AdminComponent,
    LoginComponent,
    WelcomeComponent,
    FooterComponent,
    MenuComponent,
    AdvanceHomePageContentComponent,
    ApplicationViewComponent,
    SsoresponseComponent,
    InternalServerErrorComponent,
    PageNotFoundComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule
  ],
  exports:[
    WelcomeComponent,
    FooterComponent,
    MenuComponent
  ]
})
export class AdminModule { }
