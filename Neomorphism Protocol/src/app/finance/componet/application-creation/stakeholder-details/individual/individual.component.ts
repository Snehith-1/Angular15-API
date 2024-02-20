import { Component, EventEmitter } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { CardAnimation } from "src/app/animation";
import { CustomEmailValidator, NumberInputValidator } from "src/app/shared/validators/customFormValidators";
import { LoanManagementModel } from "../../../../model/loan-management.model";
import { SocketService } from 'src/app/shared/services/socket.service';
import { UikitComponent } from "src/app/shared/component/uikit/uikit.component";

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

  interface IaddressDetails{
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
    selector: 'app-stack-individual',
    templateUrl: './individual.component.html',
    styleUrls: ['./individual.component.scss'],
    animations : [CardAnimation]
  })
  
  export class StackIndividualComponent extends EventEmitter {


  DetailsFormGroup!:FormGroup;
  institutionFormDetails!:IInstitutionFormDetails;
  addressDetails!:IaddressDetails;
  isDesignationSelected: boolean = false;
  isContactTypeSelected:boolean = false;
  contactsTypeDropDown:any;
  designationDropdown: any;
  addressDropdown: any;
  isAddSelected: boolean = false;
  GSTdata:any[]= [];
  docUpload:any[]=[];
  docObj={ };
  gstObj = { };
  geneticObj = {}
  gstApproved:boolean = false;
  gstApprovedStatus = 'No';
  geneticCodeData:any = [];
  contactsData:any =[];
  listData:any =[];
  contactObj = [];
  isContactSelectedStatus = 'Yes'
  wtContactStsChecked:boolean = false;
  stake:string = '';
  isInstitutionPage:boolean = false;
  isIndividualPage:boolean = false;

  searchAddress:any;

  gstAutoPopulate=[];




  addressObj:any;
  tableAddress:any;
  address:any;

  searchDesignations:any;
  designations:any;

  searchContactType:any;
  contactsType:any
    StakeholderSelectionTableData: any=[{
      status:true,
      guarantor:false
    }];
    stakeholderSelectionDataObj:any = {
      status:true,
      guarantor:false

    };
    StakeholderSelectionTableHeader :any;
    stakeholderSelectionTableInvalid = false;
    stakeholderSectionTableFormGroup!:FormGroup;
    addressDetailsFormGroup!:FormGroup;
    basicFormGroup!:FormGroup;
    basicFormInValid:boolean = false;
    isMartialStatusSelected:boolean = false;
    martialStatusDropdown:any;
    martialStatusArray: any;
    isLSMURNSelectedStatus: any;
    isGenderSelected: boolean = false;
    genderDropdown: any;
    listDetailsFormGroup!:FormGroup;  

genders: any;
    physicalStatusDropdown: any;
    isPhysicalStatusSelected: boolean = false;
    physicalStatusArray: any;
    branchsDropdown: any;
    isBranchSelected: boolean = false;
    nearestSamunnatiBranches: any;
    addressDetailsFormInvalid = false;
    contactPersonDetailsFormInvalid = false;
    DetailsFormInvalid = false;
    contactDetailsFormGroup!:FormGroup;
    contactTableHeader :any;

/** Paralax card effect */
cardTitles:any;
fragmentIndex:number = 0;
subMenuTitle: string = 'List';
subMenuArray:any = [];
subMenuIndex:number = 0;
mainMenu:any=[];
    isCoApplicantSelected: boolean = false;
    CoApplicantDropdown: any;
    CoApplicantArray:any;
    contactTableInvalid: boolean = false;
    response: any;
    dataaddcount: any;
    guarantor: any;
    application_gid: any;

  constructor(public loanManagementModel:LoanManagementModel, private route:ActivatedRoute, private router:Router, public socketservice:SocketService,public cmnfunctionService:UikitComponent) {

    super();
    this.CoApplicantArray = loanManagementModel.CoApplicantArray
    this.physicalStatusArray = loanManagementModel.physicalStatusArray
    this.nearestSamunnatiBranches = loanManagementModel.nearestSamunnatiBranches
    this.cardTitles = loanManagementModel.stackholderIndividualCardDetails
    this.contactTableHeader = loanManagementModel.contactTableHeader
    this.genders = loanManagementModel.genders;
    this.designations = loanManagementModel.designations;
    this.addressObj = loanManagementModel.addressObj
    this.tableAddress = loanManagementModel.tableAddress
    this.address = loanManagementModel.addressDropdown
    this.contactsType = loanManagementModel.contactsType
    this.StakeholderSelectionTableHeader = loanManagementModel.StakeholderSelectionTableHeader
    this.martialStatusArray = loanManagementModel.martialStatusArray


    this.router.navigate( [ 'app/application-creation/Stakeholder Details' ], { fragment: this.subMenuTitle } );


    loanManagementModel.ApplicationCreationMenu[2].subMenu = [
      {
        subMenuTitle:'List',
        hasError:false,
        completed:false,
      },
      {
        subMenuTitle:'StakeHolder Details',
        hasError:false,
        completed:false,
      }
    ]

    route.fragment.subscribe((fragment:any)=>{
      if(fragment !== null){
        this.subMenuTitle = fragment;
        this.DetailsFormGroup;
        this.cardTitles;
        this.fragmentIndex = this.cardTitles.indexOf(fragment);
      }
    });

    this.loadForm();
  }
  selectMartialStatus(value:any,i?:any){
    this.isMartialStatusSelected = true;
    this.martialStatusDropdown = value;
    this.setMartialStatus()
  }

  /** Paralax card effect */

  parallaxCardClick(cardFragment:any){
    this.router.navigate( [ 'app/application-creation/Stakeholder Details' ], { fragment: cardFragment } );
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

  loadForm(){
    this.DetailsFormGroup = new FormGroup({
      lmsUrnStatus : new FormControl(true, [Validators.required]),
      lmsUrn : new FormControl(''),
      fatherName : new FormControl(''),
      dateOfBirth : new FormControl(''),
      age : new FormControl(''),
      aadharNumber : new FormControl('',[Validators.minLength(12),Validators.maxLength(12),Validators.pattern(/^[2-9]{1}[0-9]{3}[0-9]{4}[0-9]{4}$/)]),
      martialStatus : new FormControl('', [Validators.required]),
      politicallyExposedPersonStatus : new FormControl(true, [Validators.required]),
      politicalyExposedPerson : new FormControl(''),
      coApplicantStatus : new FormControl(true, [Validators.required]),
      CoApplicant : new FormControl('',[Validators.required])
    })


    this.stakeholderSectionTableFormGroup = new FormGroup({
      stakeholderSectionData : new FormControl([],[Validators.required])
    })

    this.addressDetailsFormGroup = new FormGroup({
      postalCode:new FormControl(''),
      country:new FormControl(''),
      state:new FormControl(''),
      city:new FormControl(''),
      district:new FormControl(''),
      taluka:new FormControl(''),
      addressLine1:new FormControl(''),
      addressLine2:new FormControl(''),
    })


    this.contactDetailsFormGroup = new FormGroup({
      mobileNumber:new FormControl(null,[
        Validators.required,
        NumberInputValidator(10)
      ]),
      mobileNumberPrimaryStatus:new FormControl(true,[
        Validators.required
      ]),
      email:new FormControl(null,[
        Validators.required,
        CustomEmailValidator(100)
      ]),
      emailPrimaryStatus:new FormControl(true,[
        Validators.required,
      ])
     })

     
 this.listDetailsFormGroup = new FormGroup({
  SampleName:new FormControl(null,[
    Validators.required,Validators.pattern(/^[a-zA-Z ]*$/)
  ]),
  AddSampleName:new FormControl(null,[
    Validators.required,Validators.pattern(/^[a-zA-Z ]*$/)
  ]),
  SampleDesignation:new FormControl(null,[
    Validators.required,Validators.pattern(/^[a-zA-Z ]*$/)

  ]),
  AddSampleDesignation:new FormControl(null,[
    Validators.required,Validators.pattern(/^[a-zA-Z ]*$/)

  ]),
  panvalue:new FormControl(null,[
    Validators.required,Validators.maxLength(10),Validators.pattern(/^[A-Z]{3}[P]{1}[A-Z]{1}[0-9]{4}[A-Z]{1}$/)

  ]),
  AddPanValue:new FormControl(null,[
    Validators.required,Validators.pattern(/^[A-Z]{3}[P]{1}[A-Z]{1}[0-9]{4}[A-Z]{1}$/)

  ]),

 })

  }


  get lmsUrnStatus (){
    return this.DetailsFormGroup.get('lmsUrnStatus')!;
  }
  get lmsUrn (){
    return this.DetailsFormGroup.get('lmsUrn')!;
  }
  get fatherName (){
    return this.DetailsFormGroup.get('fatherName')!;
  }
  get dateOfBirth (){
    return this.DetailsFormGroup.get('dateOfBirth')!;
  }
  get age (){
    return this.DetailsFormGroup.get('age')!;
  }
  get aadharNumber (){
    return this.DetailsFormGroup.get('aadharNumber')!;
  }
  get martialStatus (){
    return this.DetailsFormGroup.get('martialStatus')!;
  }
  setMartialStatus(){
    this.martialStatus.setValue(this.martialStatusDropdown);
  }
  get politicallyExposedPersonStatus (){
    return this.DetailsFormGroup.get('politicallyExposedPersonStatus')!;
  }
  get politicalyExposedPerson (){
    return this.DetailsFormGroup.get('politicalyExposedPerson')!;
  }
  get coApplicantStatus (){
    return this.DetailsFormGroup.get('coApplicantStatus')!;
  }
  get CoApplicant (){
    return this.DetailsFormGroup.get('CoApplicant')!;
  }
  setCoApplicant(){
    this.CoApplicant.setValue(this.CoApplicantDropdown)
  }

  get stakeholderSectionData(){
    return this.stakeholderSectionTableFormGroup.get('stakeholderSectionData')!;
  }

  setStakeholderSectionData(){
    this.stakeholderSectionData.setValue(this.StakeholderSelectionTableData)
  }

  get postalCode(){
    return this.addressDetailsFormGroup.get('postalCode')!;
  }
  get country(){
    return this.addressDetailsFormGroup.get('country')!;
  }
  get state(){
    return this.addressDetailsFormGroup.get('state')!;
  }
  get city(){
    return this.addressDetailsFormGroup.get('city')!;
  }
  get district(){
    return this.addressDetailsFormGroup.get('district')!;
  }
  get taluka(){
    return this.addressDetailsFormGroup.get('taluka')!;
  }
  get addressLine1(){
    return this.addressDetailsFormGroup.get('addressLine1')!;
  }
  get addressLine2(){
    return this.addressDetailsFormGroup.get('addressLine2')!;
  }

  get mobileNumber(){
    return this.contactDetailsFormGroup.get('mobileNumber')!;
  }
  get mobileNumberPrimaryStatus(){
    return this.contactDetailsFormGroup.get('mobileNumberPrimaryStatus')!;
  }
  get email(){
    return this.contactDetailsFormGroup.get('email')!;
  }
  get emailPrimaryStatus(){
    return this.contactDetailsFormGroup.get('emailPrimaryStatus')!;
  }
  get SampleName(){
    return this.listDetailsFormGroup.get('SampleName')!;
  }
  get AddSampleName(){
    return this.listDetailsFormGroup.get('AddSampleName')!;
  }
  get panvalue(){
    return this.listDetailsFormGroup.get('panvalue')!;
  }
  get AddPanValue(){
    return this.listDetailsFormGroup.get('AddPanValue')!;
  }
  get SampleDesignation(){
    return this.listDetailsFormGroup.get('SampleDesignation')!;
  }
  get AddSampleDesignation(){
    return this.listDetailsFormGroup.get('AddSampleDesignation')!;
  }

  selectDesignations(designation:any){
    this.designationDropdown = designation;
    this.isDesignationSelected = true;
  }

  selectContactType(type:any,index:any){
    this.isContactTypeSelected = true;
    this.contactsTypeDropDown = type;
    this.contactsData[index].type = type;
  }

  selectListType(type:any,index:any){
    this.isContactTypeSelected = true;
    this.contactsTypeDropDown = type;
    this.listData[index].type = type;
  }
  selectCoApplicant(value:any){
    this.isCoApplicantSelected = true;
    this.CoApplicantDropdown = value;
    this.setCoApplicant();
  }

  trackByFn(index:any) {
    return index;
  }

  delAddData(i:number){
    this.tableAddress.splice(i,1);
  }
  delContact(i:any){
    this.contactsData.splice(i,1);
  }
  delList(i:any){
    this.listData.splice(i,1);
  }
  addContact(){
    for(let i=0; i<this.contactsData.length;i++){
      if(this.contactsData[i].Particulars == null || this.contactsData[i].type == null){
        this.contactTableInvalid = true;
        
      }else{
        this.contactTableInvalid = false;
      }
    }

    switch (this.mobileNumber.valid && this.email.valid) {
      case true:
        if(!this.contactTableInvalid || this.contactsData.length == 0){
          this.contactTableInvalid = false;
          this.contactsData.push(this.contactObj);
        }
        break;
      case false:
        this.contactTableInvalid = true;
        break;
    }

  }

  addAddressDetails()
  {
    this.tableAddress.push(this.addressObj);
  }




  checkBoxEvent(){
    this.wtContactStsChecked = !this.wtContactStsChecked;
  }
  onInputChanged(value: any, rowIndex: number, propertyKey: string): void {
    const newValue = this.StakeholderSelectionTableData.map((row: any, index: number) => {
      return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
    })
    this.StakeholderSelectionTableData = newValue;
    this.setStakeholderSectionData()
}
onInputChangedContactDetails(value: any, rowIndex: number, propertyKey: string): void {
  const newValue = this.contactsData.map((row: any, index: number) => {
    return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
  })
  this.contactsData = newValue;
}

onInputChangedListDetails(value: any, rowIndex: number, propertyKey: string): void {
  const newValue = this.listData.map((row: any, index: number) => {
    return index !== rowIndex? row: {...row, [propertyKey]: value.target.value}
  })
  this.listData = newValue;
}
addStackholderSelection(){

  for(let i=0;i<this.StakeholderSelectionTableData.length;i++){
    var data = this.StakeholderSelectionTableData[i]
    if(data.name==null || data.pan==null || data.designation==null || data.guarantor==null || data.status==null ||
       data.name=='' || data.pan=='' || data.designation=='' ){
      this.stakeholderSelectionTableInvalid = true;
      break;
    }else{
      this.stakeholderSelectionTableInvalid = false;
    }
  }
  if(!this.stakeholderSelectionTableInvalid || this.StakeholderSelectionTableData.length == 0){
    this.StakeholderSelectionTableData.push(this.stakeholderSelectionDataObj)
  }

}


  nextButton(){
    for(let i=0;i<this.StakeholderSelectionTableData.length;i++){
      var data = this.StakeholderSelectionTableData[i]
      if(data.name==null || data.pan==null || data.designation==null || data.guarantor==null || data.status==null ){
        this.stakeholderSelectionTableInvalid = true;
        break;
      }else{
        this.stakeholderSelectionTableInvalid = false;
      }
    }

    if(!this.stakeholderSelectionTableInvalid && this.StakeholderSelectionTableData.length !== 0){
      this.stakeholderSelectionTableInvalid = false;
      this.parallaxCardClick(this.secondCard());
      this.loanManagementModel.ApplicationCreationMenu[2].subMenu[0].completed = true;
      this.loanManagementModel.ApplicationCreationMenu[2].subMenu[0].hasError = false;
    }else{
      this.stakeholderSelectionTableInvalid = true;
    }

  }
  nextSection(){
    if(this.DetailsFormGroup.valid && this.addressDetailsFormGroup.valid && this.contactDetailsFormGroup.valid){
      this.addressDetailsFormInvalid = false;
      this.contactPersonDetailsFormInvalid = false;
      this.DetailsFormInvalid = false;

      this.loanManagementModel.ApplicationCreationMenu[2].subMenu[1].completed = true;
      this.loanManagementModel.ApplicationCreationMenu[2].subMenu[1].hasError = false;
      this.router.navigateByUrl('app/application-creation/Facility & Charges');
    }else{
      this.addressDetailsFormInvalid = true;
      this.contactPersonDetailsFormInvalid = true;
      this.DetailsFormInvalid = true;
      this.loanManagementModel.ApplicationCreationMenu[2].subMenu[1].hasError = true;
      this.loanManagementModel.ApplicationCreationMenu[2].subMenu[0].hasError = this.stakeholderSectionTableFormGroup.invalid;

    }
  }
  delStackholderSelection(ind:number)
  {
    this.StakeholderSelectionTableData.splice(ind,1);
  }
  ngOnInit(): void { 
    var param = {
      application_gid:  this.application_gid ,
    }
    var url = 'MstNgApplicationAdd/stakeholderindividuallistsummary';
    this.socketservice.getparams(url, param).subscribe((result:any)=>{
      this.response=result; 

      debugger;
      this.listData = this.response.mststakecontact_list; 
      this.AddPanValue.setValue(this.response.mststakecontact_list.pan_no); 
    
      for(let i=0; i<this.listData.length;i++){
        this.AddPanValue.setValue(''); 
      this.AddSampleName.setValue('')
      this.AddSampleDesignation.setValue('')
      this.AddPanValue.setValue(this.listData[i].pan_no); 
      this.AddSampleName.setValue(this.listData[i].customer_name)
      this.AddSampleDesignation.setValue(this.listData[i].designation_name)
      }
      this.dataaddcount=this.listData.length+1;
    });
    // this.route.queryParams.subscribe(params => {

    //   const hash = params['hash'];  

    //   if (hash) {

    //     const searchObject = this.cmnfunctionService.decryptURL(hash);        

    //     this.application_gid = searchObject.lsapplication_gid;

    //     }

    //   });
  
  }
  checkguarantor(contact_gid:any, guarantor:any){
    debugger 
    var CheckHeadstatus;
    guarantor = (guarantor == true ? 'Yes' : 'No');
     CheckHeadstatus = this.listData.findIndex(
      guarantor== "Yes" && guarantor == "Yes" &&
        contact_gid != contact_gid);   
        if(CheckHeadstatus != -1){
        
         this.guarantor.setValue(this.listData[CheckHeadstatus].contact_gid)
         guarantor:true
        
        }
  
      }
  // }
//   checkGuarantor(contact_gid: any, isChecked: boolean) {
//     debugger;
//     if (isChecked) {
//         const foundIndex = this.listData.findIndex((item: { guarantor: string; contact_gid: any; }) => item.guarantor === 'Yes' && item.contact_gid !== contact_gid);
//         if (foundIndex !== -1) {
//             this.guarantor.setValue(this.listData[foundIndex].contact_gid);
//             // Assuming `guarantor` is a form control, update its value
//             // Make sure you have declared `guarantor` as a FormControl in your component
//         }
//     }
// }

  // checkguarantor(contact_gid: any, guarantor: any) {
  //   debugger;
  //   var CheckHeadstatus;
  //   guarantor = guarantor ? 'Yes' : 'No';
  //   CheckHeadstatus = this.listData.findIndex(
  //     (item: { guarantor: string; contact_gid: any; }) => item.guarantor === 'Yes' && item.contact_gid !== contact_gid
  //   );
  //   if (CheckHeadstatus !== -1) {
  //     this.guarantor.setValue(true);
  //     this.listData[CheckHeadstatus].guarantor = 'Yes';
  //   }
  // }
  addList(){
    for(let i=0; i<this.listData.length;i++){
      if(this.listData[i].type == null){
        this.contactTableInvalid = true;
      }
      else{
        this.contactTableInvalid = false;
      }
    }

    switch ( this.SampleName.valid) {
      case true:
        if(!this.contactTableInvalid || this.listData.length == 0){
          this.contactTableInvalid = false;
          this.listData.push(this.contactObj);
        }
        break;
      case false:
        this.contactTableInvalid = true;
        break;
    }
  }

}