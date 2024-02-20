import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoanManagementModel } from '../../../model/loan-management.model';
import { CardAnimation } from "../../../../animation";
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SocketService } from '../../../../../app/shared/services/socket.service';
import { AppComponent } from '../../../../app.component';
interface IBasicFormDetails {
  gender:string;
  stakeholder:string;
}
@Component({
  selector: 'app-bureau',
  templateUrl: './bureau.component.html',
  styleUrls: ['./bureau.component.scss'],
  animations : [CardAnimation]
})
export class BureauComponent implements OnInit {
  bureauscore:any
  stakeholderbureauscore:any
  bureauresponse:any
  date:any
  stackholderDetails:any
  borrowertableDetails:any
  cardTitles: any = ['Borrower Summary','StakeHolder Summary'];
  fragmentIndex: number = 0;
  subMenuTitle: string = 'Borrower Summary';
  subMenuArray: any = [];
  subMenuIndex: number = 0;
  contactsData: any = [];
  contactsType: any = [
    "one","two","three"
  ];
  BorrowerHeader:any;
  BorrowerData:any[]=[
    {}
  ];
  StakeholderHeader:any;
  StakeholderData:any[]=[
    {}
  ];
  basicFormDetails: IBasicFormDetails;
  response: any;
  basicFormGroup: any;
  genderDropdown: any ;
  
  genderName: any;
  stakeholdername:any;
  stakeholderdropdown:any;
  isGenderSelected: boolean = false;
  isstakeholderSelected:boolean=false;
  
  basicFormInValid:boolean = false;
  genders: any;
  stakeholders:any;
  documentsArray: any;
  isuploadSelected: boolean =false;
  isuploadselect: boolean= true;
  isUnchanged=true;
  result:boolean=false;
  borrower_list:any[]=[];
  stackholder_list:any[]=[];
  initiate_status:any;
  view_status:any;
  disabled: any;
  ed:any;
  label:any;
  showFirstButton=true; 
constructor(public loanManagementModel: LoanManagementModel,public application:AppComponent, private route: ActivatedRoute,public notify:AppComponent, public socketservice:SocketService,private router: Router){
  this.borrowertableDetails = loanManagementModel.borrowertableDetails;
  this.stackholderDetails = loanManagementModel.stackholderDetails;
  this.BorrowerHeader=loanManagementModel.BorrowerHeader
  this.StakeholderHeader=loanManagementModel.StakeholderHeader
 // this.documentsArray = uploadDocuments;
this.date=(new Date()).toISOString().substring(0,10)
// this.date=(new Date()).toLocaleString()
  this.router.navigate(['app/application-creation/Bureau'], { fragment: this.subMenuTitle });

  loanManagementModel.ApplicationCreationMenu[5].subMenu = [
    {
      subMenuTitle: 'Borrower Summary',
      hasError: false,
      completed: false,
    },
    {
      subMenuTitle: 'StakeHolder Summary',
      hasError: false,
      completed: false,
    }
]
this.basicFormDetails = {} as IBasicFormDetails;

route.fragment.subscribe((fragment: any) => {
    if (fragment !== null) {
      this.subMenuTitle = fragment;
      this.cardTitles;
      this.fragmentIndex = this.cardTitles.indexOf(fragment);
    }
});
}
tracker= (i: any) => i;


onInputChangedBorrowerDetails(value: any, rowIndex: number, propertyKey: string): void {

  const newValue = this.BorrowerData.map((row: any, index: number) => {
    return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
  })
  // this.dataChanged.emit(newValue);
  this.BorrowerData = newValue;
  // this.setGSTDetails()
 // this.setLivestockTable();
}

onInputChangedContactDetails(value: any, rowIndex: number, propertyKey: string): void {
  const newValue = this.contactsData.map((row: any, index: number) => {
    return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
  })
  this.contactsData = newValue;
  //this.setGstTable();
}
selectborrowerlist(type:any,index:any){
  // this.isContactTypeSelected = true;
  // this.contactsTypeDropDown = type;
  this.contactsData[index].type = type;
}
parallaxCardClick(cardFragment: any) {
  this.router.navigate(['app/application-creation/Bureau'], { fragment: cardFragment });
}



secondCard(){
  if (this.fragmentIndex > this.cardTitles.length - 1) {
    return this.cardTitles[this.cardTitles.length - this.fragmentIndex - 1];
  } else if (this.fragmentIndex == this.cardTitles.length - 1) {
    return this.cardTitles[0];
  } else {
    return this.cardTitles[this.fragmentIndex + 1];
  }
}



butonchange()
  {
    this.initiate_status=false;
    this.view_status=true;
  }
upload(){
 this.isuploadselect = false;
}
show()
{
  this.ed=true;

}

deleteDocument(index:any){
  this.documentsArray[index].isUploaded = false;
}
fileUploadFunction(index:any){
    
  this.documentsArray[index].isUploaded = true;
}

backButton(){
  this.router.navigateByUrl('app/application-creation/Documents');
}
// addfamilyDetails(familyDetails?:any){
//   for(let i=0; i<this.familyData.length; i++){
//     if(this.familyData[i].relationshipType == null || this.familyData[i].firstName == null || this.familyData[i].lastName == null || this.familyData[i].nominee == null || this.familyData[i].dateOfBirth == null || this.familyData[i].age == null){
//       this.familyTableInValid = true;
//       break;
//     }else{
//       this.familyTableInValid = false;
//     }
//   }
//   if(this.familyDetailsFormGroup.invalid){
//     this.familyTableInValid = true;
//   }
//   if(!this.familyTableInValid && this.familyDetailsFormGroup.valid){
//     this.familyData.push(this.familyObj);
//   }
// }

// nextButton(){
//   if(this.subMenuTitle == 'Borrower Summary'){
    
//     switch (this.BureauDetailsFormGroup.invalid) {
//       case true:
//         this.basicFormInValid = true;
//         break;
//       case false:
//         this.basicFormInValid = false;
//         this.parallaxCardClick(this.secondCard());
//         this.loanManagementModel.ApplicationCreationMenu[1].subMenu[0].completed = true;
//         this.loanManagementModel.ApplicationCreationMenu[1].subMenu[0].hasError = false;
//         break;
//     }
//   }
loadForm(){
  this.basicFormGroup = new FormGroup({
  gender: new FormControl('', [Validators.required]),
  gender_gid : new FormControl(),

})
}
  
get gender(){
  return this.basicFormGroup.get("gender")!;
}

setGender(){
  this.gender.setValue(this.genderDropdown);
}
selectgender(gender:any){
  this.genderDropdown = gender.bureauname_name;
  this.genderName = gender.bureauname_name;
  this.isGenderSelected = true;
  this.setGender()
}
setstakeholder(){
  this.stakeholders.setValue(this.stakeholderdropdown);
}
selectstakeholder(stackholder_list:any){
  this.stakeholderdropdown = stackholder_list.name;
  this.stakeholdername = stackholder_list.name;
  this.isstakeholderSelected = true;
  this.setstakeholder()
}


ngOnInit(): void {
 this.showFirstButton=false;
  this.socketservice.get("MstApplication360/GetBureauName").subscribe((result:any)=>{
    this.response=result; 
    this.genders = this.response.application_list;
    console.log(this.genders);
  })
  var application_gid ='APPC20230626839';
  var params = {

    application_gid:application_gid

  }

  this.socketservice.getparams('MstNgApplicationAdd/GetapplicationBureauList',params).subscribe((result:any)=>{
    this.response=result; 
    this.stakeholders = this.response.Bureau_list;
    console.log(this.stakeholders);

      });

  var application_gid ='APPC20230626839';
 
  
  
    var params = {

      application_gid:application_gid

    }

    this.application.uilock();

    this.socketservice.getparams('MstNgApplicationAdd/GetapplicationBureauList',params).subscribe((result:any)=>{

      if(result.status == true){

        this.borrower_list = [];

        this.stackholder_list = [];

        for(var i=0; i<result.Bureau_list.length;i++){

          if(result.Bureau_list[i].stakeholder_type == 'Applicant'){

            this.borrower_list.push(result.Bureau_list[i]);

          }

          else{

            this.stackholder_list.push(result.Bureau_list[i]);

          }

        }

      }

      this.application.uiunlock();

    });
  }

  // Bureau Borrower summary

  highmarkdetails(type: any,stackholder_gid:any){
    this.showFirstButton =true;
    this.label=true;
    
    var application_gid ='APPC2023041065';
   
if(type=='institution'){
    var params = {
      institution_gid:stackholder_gid
    }
    this.application.uilock();
    this.socketservice.getparams('BureauAPI/GetHighmarkInstitutionCreditInfo',params).subscribe((result:any)=>{
      
      if(result.status == true){
        this.bureauscore=result.bureau_score
        // this.bureauresponse=result.bureau_response
        this.notify.showToastMessage('success',result.message);

      }
      else{
        this.notify.showToastMessage('info',result.message);
       
      }
      this.application.uiunlock();
    });
  }
else if(type=='individual')
{
  var param = {

    contact_gid:stackholder_gid
    
  }
  this.application.uilock();
  this.socketservice.getparams('BureauAPI/GetHighmarkCreditInfo',param).subscribe((result:any)=>{
   
    if(result.status == true){
      this.bureauscore=result.bureau_score
      // this.bureauresponse=result.bureau_response
      this.notify.showToastMessage('success',result.message);

    }
    else{
      this.notify.showToastMessage('info',result.message);
      
    }
    this.application.uiunlock();

    
  });

}
  }

  // Bureau Stakeholder summary
  stakeholderhighmarkdetails(type: any,stackholder_gid:any){
    this.showFirstButton =true;
    
    
    var application_gid ='APPC20230626839';
   
if(type=='institution'){
    var params = {
      institution_gid:stackholder_gid
    }
    this.application.uilock();
    this.socketservice.getparams('BureauAPI/GetHighmarkInstitutionCreditInfo',params).subscribe((result:any)=>{
      
      if(result.status == true){
        this.stakeholderbureauscore=result.bureau_score
        // this.bureauresponse=result.bureau_response
        this.notify.showToastMessage('success',result.message);

      }
      else{
        this.notify.showToastMessage('info',result.message);
      }
      this.application.uiunlock();
    });
  }
else if(type=='individual')
{
  var param = {

    contact_gid:stackholder_gid
    
  }
  this.application.uilock();
  this.socketservice.getparams('BureauAPI/GetHighmarkCreditInfo',param).subscribe((result:any)=>{
    
    if(result.status == true){
      this.stakeholderbureauscore=result.bureau_score
      // this.bureauresponse=result.bureau_response
      this.notify.showToastMessage('success',result.message);

    }
    else{
      this.notify.showToastMessage('info',result.message);
      
    }
    this.application.uiunlock();


    
  });

}
  }
}



