import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CardAnimation } from 'src/app/animation';
import { UikitComponent } from 'src/app/shared/component/uikit/uikit.component';
import { LoanManagementModel } from '../../../model/loan-management.model';

@Component({
  selector: 'app-borrower-details',
  templateUrl: './borrower-details.component.html',
  styleUrls: ['./borrower-details.component.scss'],
  animations : [CardAnimation]
})
export class BorrowerDetailsComponent implements OnInit {
  isInstitutionPage:boolean = true;
  isIndividualPage:boolean = false;
  borrower:string = '';
  borrowerDetailsFormGroup!:FormGroup;
  application_gid: any;

  constructor( public datePipe:DatePipe, private router:Router, private route:ActivatedRoute, private loanManagementModel:LoanManagementModel,public cmnfunctionService:UikitComponent){
   
    this.loadForm();
  }
loadForm(){
  this.borrowerDetailsFormGroup = new FormGroup({
    borrowerType: new FormControl('- Institution'),
    fileUpload: new FormControl('')
  })
}
  borrowerChange(e:any){
    this.borrower = e.target.value;
    if(this.borrower == '- Institution'){
      this.isInstitutionPage = true;
      this.isIndividualPage = false;
    }else{
      this.isInstitutionPage = false;
      this.isIndividualPage = true;
    }
  }
 



  // dateValidation(e:any){
  //    var newDate = new Date(e.value);

  //   if(newDate.getFullYear()<=this.todayDate.getFullYear()){
  //     if(newDate.getMonth()<=this.todayDate.getMonth()){
  //       if(newDate.getDate()<=this.todayDate.getDate()){
  //         return false;
  //       }else{
  //       return true;
  //     }
  //     }else{
  //       return true;
  //     }
  //   }else{
  //     return true;
  //   }
  // }





  saveDraft(formData:any){
  }
  



  excelUpload(abc:any)
  {
    if(abc.target.value)
    {
      alert("File Uploaded Successfully");
    }
    else
    {
      alert("Please select a File");
    }
  }
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      debugger
            const hash = params['hash'];  
      
            if (hash) {
      
              const searchObject = this.cmnfunctionService.decryptURL(hash);        
      
              this.application_gid = searchObject.lsapplication_gid;
              
      
              }
  }
    )}

}
