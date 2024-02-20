import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FinanceRoutingModule } from './finance-routing.module';
import { ApplicationSummaryComponent } from './componet/application-summary/application-summary.component';
import { FinanceComponent } from './finance.component';
import { AdminComponent } from '../admin/admin.component';
import { AdminModule } from '../admin/admin.module';
import { ExportComponent } from './componet/export/export.component';
import { ApplicationCreationComponent } from './componet/application-creation/application-creation.component';
import { GeneralInfoComponent } from './componet/application-creation/general-info/general-info.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BorrowerDetailsComponent } from './componet/application-creation/borrower-details/borrower-details.component';
import { BureauComponent } from './componet/application-creation/bureau/bureau.component';
import { DocumentsComponent } from './componet/application-creation/documents/documents.component';
import { FacilityAndChargesComponent } from './componet/application-creation/facility/facility.component';
import { StakeholderDetailsComponent } from './componet/application-creation/stakeholder-details/stakeholder-details.component';
import { SubmissionComponent } from './componet/application-creation/submission/submission.component';
//import { ApplicationCreationComponent } from './componet/application-creation/application-creation.component';
import { StackIndividualComponent } from './componet/application-creation/stakeholder-details/individual/individual.component';
import { StackInstitutionComponent } from './componet/application-creation/stakeholder-details/institution/institution.component';
import { IndividualComponent } from './componet/application-creation/borrower-details/individual/individual.component';
import { InstitutionComponent } from './componet/application-creation/borrower-details/institution/institution.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { UikitComponent } from '../shared/component/uikit/uikit.component';
import { SharedModule } from '../shared/shared.module';
import { ApplicationEditComponent } from './componet/application-edit/application-edit.component';
import { GeneralinfoeditComponent } from './componet/application-edit/generalinfoedit/generalinfoedit.component';
import { BorrowerdetailseditComponent } from './componet/application-edit/borrowerdetailsedit/borrowerdetailsedit.component';
import { IndividualeditComponent } from './componet/application-edit/borrowerdetailsedit/individualedit/individualedit.component';
import { InstitutioneditComponent } from './componet/application-edit/borrowerdetailsedit/institutionedit/institutionedit.component';

@NgModule({
  declarations: [
    ApplicationSummaryComponent,
    FinanceComponent,
    ExportComponent,
    GeneralInfoComponent,
    BorrowerDetailsComponent,
    BureauComponent,
    DocumentsComponent,
    StakeholderDetailsComponent,
    SubmissionComponent,
    ApplicationCreationComponent,
    StackIndividualComponent,
    StackInstitutionComponent,
    IndividualComponent,
    InstitutionComponent,
    FacilityAndChargesComponent,
    ApplicationEditComponent,
    GeneralinfoeditComponent,
    BorrowerdetailseditComponent,
    IndividualeditComponent,
    InstitutioneditComponent,
  
  ],
  imports: [
    CommonModule,
    FinanceRoutingModule,
    AdminModule,
    FormsModule,
    ReactiveFormsModule,
    Ng2SearchPipeModule,
    SharedModule    
  ],
  providers: [

    Ng2SearchPipeModule,
    DatePipe,
    UikitComponent
  ]
})
export class FinanceModule { }
