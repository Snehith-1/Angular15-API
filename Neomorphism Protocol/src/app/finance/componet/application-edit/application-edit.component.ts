import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DropDownAnimation } from 'src/app/animation';
import { DatePipe } from '@angular/common';
import { LoanManagementModel } from '../../model/loan-management.model';
import { UikitComponent } from 'src/app/shared/component/uikit/uikit.component';

@Component({
  selector: 'app-application-edit',
  templateUrl: './application-edit.component.html',
  styleUrls: ['./application-edit.component.scss']
})

export class ApplicationEditComponent implements OnInit{
  isOpen:boolean = false;
  dropdownIndex:number = 0;
  appMenuArr:any = [];
  tableData:any = [];

  vertical:any = 'Vertical Name';

  constructor(private loanManagementModel:LoanManagementModel, private router:Router, private route:ActivatedRoute, protected datePipe: DatePipe,public descrypt:UikitComponent) {
    this.appMenuArr = loanManagementModel.ApplicationCreationMenu;
    // loanManagementModel.ApplicationCreationMenu[1].subMenu[0].subMenuTitle = "Mukilan";
    this.tableData = loanManagementModel.tableData;
   }
   trackByFn(index:any) {
    return index; // or item.id
  }
  
  ngOnInit() {
  }
  
  quickLinkNavigation(i:number, title:any){
    if(this.dropdownIndex == i){
      this.dropdownIndex = 0;
    }else{
      this.dropdownIndex = i;
    }       
    // this.router.navigateByUrl('app/welcome/finance/business/loan-management/application-creation/'+title)
    this.router.navigateByUrl('app/application-creation/'+title)
  }

  quickLinkVlidationIndication(hasError:boolean,completed:boolean){
    if(hasError){
      return 'color:#F37021'
    }else if(completed){
      return 'color:#448EE4'
    }else{
      return ''
    }
  }

  getItem(e:any){     
    this.vertical = e;
  }

  errorMark(subMenu:string){
    if(subMenu == 'General-Details' && !this.loanManagementModel.isGeneralInformationCompleted && this.loanManagementModel.isGeneralDetailsFormSubmitted){
      return 'hasError color: #dc3545;'
    }else if(subMenu == 'SPOC Details' && !this.loanManagementModel.isSPoCDetailsCompleted && this.loanManagementModel.isGeneralDetailsFormSubmitted){
      return 'hasError color: #dc3545;'
    }else if(subMenu == 'Business Activities' && !this.loanManagementModel.isBusinessActivitiesCompleted && this.loanManagementModel.isGeneralDetailsFormSubmitted){
      return 'hasError color: #dc3545;'
    }else{
      return ''
    }
  }
}
