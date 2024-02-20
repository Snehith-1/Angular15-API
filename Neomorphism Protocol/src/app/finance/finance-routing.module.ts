import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicationCreationComponent } from './componet/application-creation/application-creation.component';
import { BorrowerDetailsComponent } from './componet/application-creation/borrower-details/borrower-details.component';
import { BureauComponent } from './componet/application-creation/bureau/bureau.component';
import { DocumentsComponent } from './componet/application-creation/documents/documents.component';
import { FacilityAndChargesComponent } from './componet/application-creation/facility/facility.component';
import { GeneralInfoComponent } from './componet/application-creation/general-info/general-info.component';
import { StakeholderDetailsComponent } from './componet/application-creation/stakeholder-details/stakeholder-details.component';
import { SubmissionComponent } from './componet/application-creation/submission/submission.component';

const routes: Routes = [
  // { path: 'applicationsummary', component: ApplicationSummaryComponent },  

  // { path: 'application-creation', component: ApplicationCreationComponent,
  //   children:[
  //     {
  //       path: 'General-Details',
  //       component : GeneralInfoComponent
  //     },
  //     {
  //       path: 'Borrower Details',
  //       component : BorrowerDetailsComponent
  //     },
  //     {
  //       path: 'Stakeholder Details',
  //       component : StakeholderDetailsComponent
  //     },
  //     {
  //       path: 'Facility & Charges',
  //       component: FacilityAndChargesComponent
  //     },
  //     {
  //       path: 'Documents',
  //       component : DocumentsComponent
  //     },
  //     {
  //       path: 'Bureau',
  //       component : BureauComponent
  //     },
  //     {
  //       path: 'Submission',
  //       component : SubmissionComponent
  //     },
  //   ]
  // },
  // { path: 'application-edit', component: ApplicationEditComponent, 
  // children:[
  //   {
  //     path: 'General-Edit',
  //     component : GeneralinfoeditComponent
  //   },
  // {
  //   path: 'generalinfoedit',
  //   component: GeneralinfoeditComponent
  // },
  // {
  //   path: 'borrowerdetailsedit',
  //   component: BorrowerdetailseditComponent
  // },
  // {
  //   path: 'institutionedit',
  //   component : InstitutioneditComponent
  // },
  
//   ]
// },
//   { path: 'excel', component: ExportComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FinanceRoutingModule { }
