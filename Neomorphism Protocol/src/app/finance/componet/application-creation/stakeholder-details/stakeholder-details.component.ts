import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CardAnimation } from 'src/app/animation';
import { LoanManagementModel } from '../../../model/loan-management.model';

interface IgstDetails {
  gstNumber:string,
  gstSate:string,
  gstHeadOffice:string
  }

interface IInstitutionFormDetails {
  cin:string,
  cinDate:string,
  businessStartDate:string,
  businessVintage:string,
  panValue:string,
  legelTradeName:string,
  gstDetails:Array<IgstDetails>,
  tan:string,
  tanState:string,
  }

  interface iaddressDetails{
    postal:string,
    country:string,
    state:string,
    city:string,
    district:string,
    taluk:string,
    address:string,
    address1:string
  }



  @Component({
    selector: 'app-stakeholder-details',
    templateUrl: './stakeholder-details.component.html',
    styleUrls: ['./stakeholder-details.component.scss'],
    animations : [CardAnimation]
  })



export class StakeholderDetailsComponent implements OnInit {


  isInstitutionPage:boolean = true;
  isIndividualPage:boolean = false;
  borrower:string = '';
  stackholderDetailsFormGroup!: FormGroup;
  
  constructor( public datePipe:DatePipe, private router:Router, private route:ActivatedRoute, private loanManagementModel:LoanManagementModel){
   
    this.loadForm();
  }
loadForm(){
  this.stackholderDetailsFormGroup = new FormGroup({
    stackholderType: new FormControl('- Institution'),
    fileUpload: new FormControl('')
  })
}

get stackholderType(){
  return this.stackholderDetailsFormGroup.get('stackholderType')!;
}

get fileUpload(){
  return this.stackholderDetailsFormGroup.get('fileUpload')!;
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
  ngOnInit(): void {

  }


}




